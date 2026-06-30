using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.DigitalCard;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.DigitalCard.Interfaces;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.DigitalCard
{
    public class DigitalCardService : BaseService, IDigitalCardService
    {
        private readonly ILogger<DigitalCardService> _logger;
        private readonly IMMOnlineRepositoryWrapper _mMOnlineRepositoryWrapper;
        private readonly HttpClientConnectionModel _epayConnection;
        private readonly IEpayClientService _epayClientService;
        private readonly IUnitOfWork<EFCoreMMOnlineSqlProvider> _mmOnlineUnitOfWork;
        public DigitalCardService(IServiceProvider serviceProvider
            , IMMOnlineRepositoryWrapper mMOnlineRepositoryWrapper
            , IOptions<HttpClientConfigModel> options
            , IEpayClientService epayClientService
            , ILogger<DigitalCardService> logger
            , IUnitOfWork<EFCoreMMOnlineSqlProvider> mmOnlineUnitOfWork) : base(serviceProvider)
        {
            _mMOnlineRepositoryWrapper = mMOnlineRepositoryWrapper;
            _epayConnection = options.Value.EpayConnection;
            _epayClientService = epayClientService;
            _logger = logger;
            _mmOnlineUnitOfWork = mmOnlineUnitOfWork;
        }
        public async Task<ServiceResultModel<List<CreataSerialCodeInEpayResponseModel>>> CreataSerialCodeInEpay(CreataSerialCodeInEpayRequestModel model)
        {
            var response = new List<CreataSerialCodeInEpayResponseModel>();
            try
            {
                var isDCItem = await _mMOnlineRepositoryWrapper.DCItemRepository
                                                          .GetQuery()
                                                          .AsNoTracking()
                                                          .FirstOrDefaultAsync(a => a.CustomerOrderNumber == model.CustomerOrderNumber
                                                          && a.LineItemId == model.LineItemID);
                if (isDCItem==null)
                {
                    return Result<List<CreataSerialCodeInEpayResponseModel>>(null, "Kayıt bulunamadı!", StatusCodes.Status404NotFound);
                }

                var getDCTransactionResults = await _mMOnlineRepositoryWrapper.DCTransactionResultRepository
                                                          .GetQuery()
                                                          .AsNoTracking()
                                                          .Where(a => a.CustomerOrderNumber == model.CustomerOrderNumber
                                                          && a.LineItemId == model.LineItemID).ToListAsync();
                if(!getDCTransactionResults.Any())
                {
                    return Result<List<CreataSerialCodeInEpayResponseModel>>(null, "Kayıt bulunamadı!", StatusCodes.Status404NotFound);
                }
                var dCTransactionResultSuccessCount = getDCTransactionResults.Where(a => a.Result == "0").Count();
                var dCItemCount = isDCItem.TotalQuantity;

                if (dCTransactionResultSuccessCount== dCItemCount)
                {
                    var myModel = new CreataSerialCodeInEpayResponseModel();
                    myModel.SendingDate =  (DateTime)getDCTransactionResults.FirstOrDefault().SendingDate;
                    myModel.TransactionId = getDCTransactionResults.FirstOrDefault().TxId;

                    response.Add(myModel);

                    return Result(response);
                }
                else
                {
                    #region sendDataToEpay

                    var isDCHead = await _mMOnlineRepositoryWrapper.DCHeadRepository
                                                              .GetQuery()
                                                              .AsNoTracking()
                                                              .FirstOrDefaultAsync(a => a.CustomerOrderNumber == model.CustomerOrderNumber);
                    if (isDCHead != null)
                    {
                        for (int i = 0; i < dCItemCount - dCTransactionResultSuccessCount; i++)
                        {
                            var responseItem = new CreataSerialCodeInEpayResponseModel();
                            var myModel = new DCItemSummaryModel()
                            {
                                Id = isDCItem.Id,
                                CustomerOrderNumber = isDCItem.CustomerOrderNumber.ToString(),
                                LineItemId = isDCItem.LineItemId,
                                ProductID = isDCItem.ProductID,
                                BarcodeNo = isDCItem.BarcodeNo,
                                ProductName = isDCItem.ProductName,
                                CustomerName = isDCHead.CustomerName,
                                CustomerSurname = isDCHead.CustomerSurname,
                                CustomerEmail = isDCHead.CustomerEmail,
                                CustomerPhone = isDCHead.CustomerPhone,
                                CustomerUserId = isDCHead.CustomerUserId != default ? isDCHead.CustomerUserId.ToString() : default,
                                CustomerSalutation = isDCHead.CustomerSalutation,
                                ItemPrice = isDCItem.ItemPrice,
                                ItemPriceNet = isDCItem.ItemPriceNet,
                                Quantity = isDCItem.TotalQuantity ?? 0
                            };

                            #region xmlData

                            var txID = myModel.CustomerOrderNumber + "_" + myModel.LineItemId + DateTime.Now.ToString("HHmmssfff");
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                            sb.Append("<REQUEST TYPE=\"SALE\">");
                            sb.Append("<USERNAME>" + _epayConnection.Username + "</USERNAME>");
                            sb.Append("<PASSWORD>" + _epayConnection.Password + "</PASSWORD>");
                            sb.Append("<TERMINALID>93889334</TERMINALID>");
                            sb.Append("<TXID>" + txID + "</TXID>");
                            sb.Append("<LOCALDATETIME>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</LOCALDATETIME>");
                            sb.Append("<CARD><EAN>" + myModel.BarcodeNo + "</EAN></CARD>");//item.Barcode
                            sb.Append("<AMOUNT>" + Convert.ToInt32(myModel.ItemPriceNet).ToString() + "</AMOUNT>");
                            sb.Append("<CURRENCY>949</CURRENCY>");//currency
                            sb.Append("<RECEIPT><CHARSPERLINE>60</CHARSPERLINE></RECEIPT>");
                            sb.Append("<CONSUMER>");
                            sb.Append("<NAME>" + myModel.CustomerName + "</NAME>");
                            sb.Append("<SURNAME>" + myModel.CustomerSurname + "</SURNAME>");
                            //sb.Append("<EMAIL>" + myModel.CustomerEmail + "</EMAIL>");
                            sb.Append("<EMAIL>cabarc@mediamarkt.com.tr</EMAIL>");
                            sb.Append("<TITLE>" + myModel.CustomerSalutation + "</TITLE>");//sa_salutation
                            sb.Append("<CUSTOMERID>" + myModel.CustomerUserId + "</CUSTOMERID>");
                            sb.Append("</CONSUMER>");
                            sb.Append("</REQUEST>");


                            #endregion

                            var stringContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");

                            var sendingDate = DateTime.Now;
                            var result = await _epayClientService.PostAsync("up-interface", stringContent);
                            var createdDate = DateTime.Now;

                            if (result.RESULT.Equals("0"))
                            {
                                var entity = new DCTransactionResultEntity();

                                entity.CustomerOrderNumber = Convert.ToInt32(myModel.CustomerOrderNumber);
                                entity.LineItemId = myModel.LineItemId;
                                entity.LocalDateTime = Convert.ToDateTime(result.LOCALDATETIME);
                                entity.ServerDateTime = Convert.ToDateTime(result.SERVERDATETIME);
                                entity.TxId = txID;
                                entity.HostTxId = result.HOSTTXID;
                                entity.Amount = result.AMOUNT;
                                if (result.PINCREDENTIALS != null)
                                {
                                    entity.PinCode = !string.IsNullOrEmpty(result.PINCREDENTIALS.PIN) ? result.PINCREDENTIALS.PIN : "";
                                    entity.SerialCode = !string.IsNullOrEmpty(result.PINCREDENTIALS.SERIAL) ? result.PINCREDENTIALS.SERIAL : "";
                                }
                                entity.Result = result.RESULT;
                                entity.ResultText = result.RESULTTEXT;
                                entity.RequestData = JsonConvert.SerializeObject(sb);
                                entity.ResponseData = result.ResponseData;
                                entity.SendingDate = sendingDate;
                                entity.CreatedDate = createdDate;

                                try
                                {
                                    await _mmOnlineUnitOfWork.BeginTransactionAsync();

                                    _mMOnlineRepositoryWrapper.DCTransactionResultRepository.Create(entity);

                                    await _mmOnlineUnitOfWork.SaveChangesAsync();
                                }
                                catch (Exception ex)
                                {
                                    await _mmOnlineUnitOfWork.RollbackAsync();
                                    _logger.LogError($"<DigitalCardService>CreataSerialCodeInEpay:{ex.Message}</DigitalCardService>");
                                }
                            }
                            else
                            {
                                var entity = new DCTransactionResultEntity();

                                entity.CustomerOrderNumber = Convert.ToInt32(myModel.CustomerOrderNumber);
                                entity.LineItemId = myModel.LineItemId;
                                entity.LocalDateTime = Convert.ToDateTime(result.LOCALDATETIME);
                                entity.ServerDateTime = Convert.ToDateTime(result.SERVERDATETIME);
                                entity.TxId = txID;
                                entity.HostTxId = result.HOSTTXID;
                                entity.Amount = result.AMOUNT;
                                if (result.PINCREDENTIALS != null)
                                {
                                    entity.PinCode = !string.IsNullOrEmpty(result.PINCREDENTIALS.PIN) ? result.PINCREDENTIALS.PIN : "";
                                    entity.SerialCode = !string.IsNullOrEmpty(result.PINCREDENTIALS.SERIAL) ? result.PINCREDENTIALS.SERIAL : "";
                                }
                                entity.Result = result.RESULT;
                                entity.ResultText = result.RESULTTEXT;
                                entity.RequestData = JsonConvert.SerializeObject(sb);
                                entity.ResponseData = result.ResponseData;
                                entity.SendingDate = sendingDate;
                                entity.CreatedDate = createdDate;

                                try
                                {
                                    await _mmOnlineUnitOfWork.BeginTransactionAsync();

                                    _mMOnlineRepositoryWrapper.DCTransactionResultRepository.Create(entity);

                                    await _mmOnlineUnitOfWork.SaveChangesAsync();
                                    await _mmOnlineUnitOfWork.CommitAsync();
                                }
                                catch (Exception ex)
                                {
                                    await _mmOnlineUnitOfWork.RollbackAsync();
                                    _logger.LogError($"<DigitalCardService>CreataSerialCodeInEpay:{ex.Message}</DigitalCardService>");
                                }

                            }

                            responseItem.Message = result.RESULTTEXT;
                            responseItem.TransactionId = txID;
                            responseItem.SendingDate = sendingDate;

                            response.Add(responseItem);
                        }
                        return Result(response);
                    }
                    else
                    {

                        return Result<List<CreataSerialCodeInEpayResponseModel>>(null, "Kayıt bulunamadı!", StatusCodes.Status404NotFound);
                    }

                    #endregion

                }


            }
            catch (Exception ex)
            {
                return Result<List<CreataSerialCodeInEpayResponseModel>>(null);
            }
        }
    }
}
