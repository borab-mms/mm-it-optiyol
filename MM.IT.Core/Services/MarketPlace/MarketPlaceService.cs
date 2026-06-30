using Castle.Core.Internal;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using MM.IT.Common.Enums;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Common.Models.ESB;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace MM.IT.Core.Services.MarketPlace;

public class MarketPlaceService : BaseService, IMarketPlaceService
{
    private readonly IMMOnlineRepositoryWrapper _mMOnlineRepositoryWrapper;
    private readonly IMasterDataRepositoryWrapper _masterDataRepositoryWrapper;
    private readonly IFOMRepositoryWrapper _fOMRepositoryWrapper;
    private readonly IUnitOfWork<EFCoreMMOnlineSqlProvider> _mmOnlineUnitOfWork;
    private readonly IOptions<RedisConfigModel> _redisConfigModel;
    private readonly IRedisDistributedAdapter _redisDistributedAdapter;
    private readonly object myLock = new object();
    public MarketPlaceService(IServiceProvider serviceProvider
        , IMMOnlineRepositoryWrapper mMOnlineRepositoryWrapper
        , IUnitOfWork<EFCoreMMOnlineSqlProvider> mmOnlineUnitOfWork
        , IOptions<RedisConfigModel> redisConfigModel
        , IRedisDistributedAdapter redisDistributedAdapter
        , IFOMRepositoryWrapper fOMRepositoryWrapper
        , IMasterDataRepositoryWrapper masterDataRepositoryWrapper) : base(serviceProvider)
    {
        _mMOnlineRepositoryWrapper = mMOnlineRepositoryWrapper;
        _mmOnlineUnitOfWork = mmOnlineUnitOfWork;
        _redisConfigModel = redisConfigModel;
        _redisDistributedAdapter = redisDistributedAdapter;
        _fOMRepositoryWrapper = fOMRepositoryWrapper;
        _masterDataRepositoryWrapper = masterDataRepositoryWrapper;
    }

    #region ReturnDemands
    public async Task<ServiceResultModel<IEnumerable<KeyValueModel<int, string>>>> GetSaleChannelBySearchAsync(string search)
    {
        var entities = await _mMOnlineRepositoryWrapper.SaleChannelRepository
            .GetQuery()
            .AsNoTracking()
            .Where(a => a.Code == search)
            .Select(p => new KeyValueModel<int, string>
            {
                Key = p.Id,
                Value = p.Code
            })
            .ToListAsync();

        return Result<IEnumerable<KeyValueModel<int, string>>>(entities);
    }
    public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, string>>>> GetDemandByCriteriaAsync(ReturnDemandRequestModel model)
    {
        var entities = await _mMOnlineRepositoryWrapper.ReturnDemandRepository
            .GetQuery()
            .AsNoTracking()
            .Where(a => a.OrderHeadId == model.erpId && a.ReturnDetailId == model.returnDetailId)
            .Select(p => new KeyValueModel<string, string>
            {
                Key = p.OrderHeadId,
                Value = p.ReturnDetailId
            })
            .ToListAsync();

        return Result<IEnumerable<KeyValueModel<string, string>>>(entities);
    }
    public async Task<ServiceResultModel<ReturnDemandResponseModel>> AddReturnDemandAsync(ReturnDemandRequestModel model)
    {
        try
        {
            var response = new ReturnDemandResponseModel();

            var isDemand = await _mMOnlineRepositoryWrapper.ReturnDemandRepository
                                                          .GetQuery()
                                                          .AsNoTracking()
                                                          .FirstOrDefaultAsync(a => a.OrderHeadId == model.erpId
                                                          && a.ReturnDetailId == model.returnDetailId);

            if (isDemand != null)
            {
                return Result<ReturnDemandResponseModel>(null, "İade Talebiniz Zaten Var!", 1001);
            }

            await _mmOnlineUnitOfWork.BeginTransactionAsync();

            var entity = new ReturnDemandEntity();

            entity.Article = model.article;
            entity.ChannelCode = model.channelCode;
            entity.OrderHeadId = model.erpId;
            entity.Amount = model.amount;
            entity.ReturnReason = model.returnReason;
            entity.ReturnDate = model.returnDate;
            entity.ReturnId = model.returnId;
            entity.ReturnDetailId = model.returnDetailId;
            entity.ShippingCompany = model.shippingCompany;
            entity.CargoCode = model.cargoCode;
            entity.ReturnDemandStatuId = 0;
            entity.CreatedDate = DateTime.Now;

            _mMOnlineRepositoryWrapper.ReturnDemandRepository.Create(entity);

            await _mmOnlineUnitOfWork.SaveChangesAsync();
            await _mmOnlineUnitOfWork.CommitAsync();

            var dataItem = new DataItem() { Result = "Başarılı!" };

            response.Data = dataItem;

            return Result(response);

        }
        catch (Exception ex)
        {
            await _mmOnlineUnitOfWork.RollbackAsync();
            return Result<ReturnDemandResponseModel>(null, "Sorgu sırasında hata oluştu!", 1002);
        }
    }

    #endregion

    #region Orders
    public async Task<ServiceResultModel<OrderResponseModel>> NewOrderAsync(OrderRequestModel model)
    {
        var response = new OrderResponseModel();
        try
        {

            var isOrderHead = await _mMOnlineRepositoryWrapper.OrderHeadRepository
                                                 .GetQuery()
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(a => a.ChannelOrderNumber == model.OrderHeader.channelOrderNumber
                                                                      && a.ChannelPackageNumber == model.OrderHeader.channelPackageNumber);
            if (isOrderHead != null)
            {

                var dataOrderInOrder = new DataOrder() { orderHeadId = isOrderHead.OrderHeadId };
                response.data = dataOrderInOrder;
                response.success = true;
                response.message = "Sipariş zaten kayıtlı!";

                return Result(response);
            }

         
            var ChannelOrderStatusId = 0;


            var isSalesChannel = _mMOnlineRepositoryWrapper.SaleChannelRepository
                                                 .GetQuery()
                                                 .AsNoTracking()
                                                 .FirstOrDefault(a => a.Code == model.OrderHeader.channelCode);
            if (isSalesChannel == null)
            {
                response.message = "ChannelCode geçersiz!";
                response.success = false;
                response.code = 1002;
                return Result(response);
            }

            #region getChannelOrderStatusId

            var isStatusInSalesChannelOrderStatus = _mMOnlineRepositoryWrapper.SalesChannelOrderStatuRepository
                                                             .GetQuery()
                                                             .AsNoTracking()
                                                             .FirstOrDefaultAsync(a => a.StatusName == model.OrderHeader.channelOrderStatus);

            if (isStatusInSalesChannelOrderStatus.Result != null)
            {

                ChannelOrderStatusId = isStatusInSalesChannelOrderStatus.Result.Id;
            }
            else
            {

                await _mmOnlineUnitOfWork.BeginTransactionAsync();

                var salesChannelOrderStatuEntity = new SalesChannelOrderStatuEntity();

                salesChannelOrderStatuEntity.StatusName = model.OrderHeader.channelOrderStatus;
                salesChannelOrderStatuEntity.StatusDescription = "";
                salesChannelOrderStatuEntity.SalesChannelId = isSalesChannel.Id;
                salesChannelOrderStatuEntity.CreatedDate = DateTime.Now;

                _mMOnlineRepositoryWrapper.SalesChannelOrderStatuRepository.Create(salesChannelOrderStatuEntity);

                await _mmOnlineUnitOfWork.SaveChangesAsync();
                await _mmOnlineUnitOfWork.CommitAsync();

                ChannelOrderStatusId = salesChannelOrderStatuEntity.Id;

            }


            #endregion

            lock (myLock)
            {
                var creationDate = DateTime.Now;

                var lastSequence = isSalesChannel.StartValue;
                var currentSequence = lastSequence + 1;

                model.OrderHeader.orderHeadId = isSalesChannel.Prefix + "_" + currentSequence;
                model.OrderHeader.customerOrderNumber = currentSequence;

                _mmOnlineUnitOfWork.BeginTransaction();

                #region orderHead

                var entity = new OrderHeadEntity();

                entity.OrderHeadId = model.OrderHeader.orderHeadId;
                entity.CustomerOrderNumber = model.OrderHeader.customerOrderNumber;
                //entity.ChannelCode = model.OrderHeader.channelCode;
                entity.ChannelOrderNumber = model.OrderHeader.channelOrderNumber;
                entity.ChannelPackageNumber = model.OrderHeader.channelPackageNumber;
                entity.IntegratorOrderNumber = model.OrderHeader.IntegratorOrderNumber;
                entity.CustomerType = model.Customer.addressInformation.billingAddress.isEinvoice == true ? "B2B_SALE" : "B2C_SALE";
                entity.TotalAmount = model.Products.Sum(a => a.amount);
                entity.ChannelOrderDate = model.OrderHeader.channelOrderDate;
                entity.ChannelOrderStatusId = ChannelOrderStatusId;
                entity.ShippingCompany = model.OrderHeader.shippingCompany;
                entity.ChannelShipmentType = model.OrderHeader.channelShipmentType;
                entity.CargoCode = model.OrderHeader.cargoCode;
                entity.ShippingDue = model.OrderHeader.shippingDue;
                entity.CurrencyCode = model.OrderHeader.currencyCode;
                entity.ChannelOrderNote = model.OrderHeader.channelOrderNote;
                entity.OrderType = null;
                //entity.OutletId = null;
                //entity.WhId = null;
                entity.FomOrderStatus = null;
                //entity.FomDeliveryType = null;
                entity.FomOrderDate = null;
                //entity.UserId = null;
                //entity.UpdatedDate = null;
                entity.CreatedDate = creationDate;

                _mMOnlineRepositoryWrapper.OrderHeadRepository.Create(entity);

                #endregion

                #region orderItem

                foreach (var item in model.Products)
                {
                    var orderItemEntity = new OrderItemEntity();

                    orderItemEntity.OrderHeadId = model.OrderHeader.orderHeadId;
                    orderItemEntity.orderLineNumber = item.orderLineNumber;
                    orderItemEntity.Article = item.article;
                    orderItemEntity.ArticleName = item.name;
                    orderItemEntity.Barcode = item.barcode;
                    orderItemEntity.Discount = item.discount;
                    orderItemEntity.Quantity = item.quantity;
                    //orderItemEntity.PgId = null;
                    //orderItemEntity.PgName = null;
                    orderItemEntity.UnitAmount = item.amount;
                    orderItemEntity.TotalAmount = item.amount * item.quantity;
                    orderItemEntity.VatRate = item.vatRate;
                    orderItemEntity.ItemStatus = 1;
                    orderItemEntity.CancelledQty = 0;
                    orderItemEntity.ReturnedQty = 0;
                    //orderItemEntity.UserId = null;
                    //orderItemEntity.UpdatedDate = null;
                    orderItemEntity.CreatedDate = creationDate;

                    _mMOnlineRepositoryWrapper.OrderItemRepository.Create(orderItemEntity);

                }

                #endregion

                #region customer
                var customerEntity = new CustomerEntity();

                customerEntity.OrderHeadId = model.OrderHeader.orderHeadId;
                customerEntity.BillingAddressFirstName = model.Customer.addressInformation.billingAddress.firstName;
                customerEntity.BillingAddressLastName = model.Customer.addressInformation.billingAddress.lastName;
                customerEntity.BillingAddressEmail = model.Customer.addressInformation.billingAddress.email;
                customerEntity.BillingAddress = model.Customer.addressInformation.billingAddress.address;
                customerEntity.BillingAddressMobileNumber = model.Customer.addressInformation.billingAddress.mobileNumber;
                customerEntity.BillingAddressZipCode = model.Customer.addressInformation.billingAddress.zipCode;
                customerEntity.BillingAddressDistrict = model.Customer.addressInformation.billingAddress.district;
                customerEntity.BillingAddressCity = model.Customer.addressInformation.billingAddress.city;
                customerEntity.BillingAddressTownship = model.Customer.addressInformation.billingAddress.township;
                customerEntity.BillingAddressCompanyName = model.Customer.addressInformation.billingAddress.companyName;
                customerEntity.BillingAddressCountry = model.Customer.addressInformation.billingAddress.country;
                customerEntity.BillingAddressIsEinvoice = model.Customer.addressInformation.billingAddress.isEinvoice;
                customerEntity.BillingAddressTaxOffice = model.Customer.addressInformation.billingAddress.taxOffice;
                customerEntity.BillingAddressTaxNo = model.Customer.addressInformation.billingAddress.taxNo;
                customerEntity.BillingAddressTcKimlikNo = model.Customer.addressInformation.billingAddress.tcKimlikNo;
                customerEntity.ShippingAddressFirstName = model.Customer.addressInformation.shippingAddress.firstName;
                customerEntity.ShippingAddressLastName = model.Customer.addressInformation.shippingAddress.lastName;
                customerEntity.ShippingAddressMobileNumber = model.Customer.addressInformation.shippingAddress.mobileNumber;
                customerEntity.ShippingAddressEmail = model.Customer.addressInformation.shippingAddress.email;
                customerEntity.ShippingAddress = model.Customer.addressInformation.shippingAddress.address;
                customerEntity.ShippingAddressZipCode = model.Customer.addressInformation.shippingAddress.zipCode;
                //customerEntity.ShippingAddressCountry = model.Customer.addressInformation.shippingAddress.country;
                customerEntity.ShippingAddressDistrict = model.Customer.addressInformation.shippingAddress.district;
                customerEntity.ShippingAddressCity = model.Customer.addressInformation.shippingAddress.city;
                customerEntity.ShippingAddressTownship = model.Customer.addressInformation.shippingAddress.township;
                customerEntity.CreatedDate = creationDate;


                _mMOnlineRepositoryWrapper.CustomerRepository.Create(customerEntity);


                #endregion

                isSalesChannel.StartValue = isSalesChannel.StartValue + 1;
                _mMOnlineRepositoryWrapper.SaleChannelRepository.Update(isSalesChannel);

                _mmOnlineUnitOfWork.SaveChanges();
                _mmOnlineUnitOfWork.Commit();
                model.Customer.customerId = customerEntity.Id;

                var dataOrder = new DataOrder() { orderHeadId = model.OrderHeader.orderHeadId };
                response.data = dataOrder;
            }

            #region addDataToRedis

            var key = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderSettings.MarketPlaceKey + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var redisCategory = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderSettings.RedisCategory;
            var order_NewData = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderSettings.Order_NewData;

            if (model != null)
            {
                var redisModel = new OrderRequestRedisModel()
                {
                    OrderRequestModel = model,
                    Key = key,
                    CreatedDate = DateTime.Now
                };
                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<OrderRequestRedisModel>(redisCategory, order_NewData, redisModel, key, 0);
                if (!isSetRedisData)
                {
                    response.message = "Redis bağlantısı kurulamadı!";
                    response.success = false;
                    response.code = 1003;
                    return Result(response);
                }
            }

            #endregion


            response.success = true;
            response.message = "Başarılı!";
            return Result(response);

        }
        catch (Exception ex)
        {
            _mmOnlineUnitOfWork.Rollback();

            response.message = "Sorgu sırasında hata oluştu!";
            response.success = false;
            response.code = 1004;
            return Result(response);

        }


    }
    public async Task<ServiceResultModel<UpdateOrderStatusResponseModel>> UpdateOrderStatusAsync(UpdateOrderStatusRequestModel model)
    {
        var response = new UpdateOrderStatusResponseModel();
        try
        {
            var isSalesChannel = await _mMOnlineRepositoryWrapper.SaleChannelRepository
                                                           .GetQuery()
                                                           .AsNoTracking()
                                                           .FirstOrDefaultAsync(a => a.Code == model.ChannelCode);
            UpdateOrderStatusResponseModel responseModel = new UpdateOrderStatusResponseModel();
            if (isSalesChannel == null)
            {
                response.message = "ChannelCode geçersiz!";
                response.success = false;
                response.code = 1001;
                return Result(response);
            }
            var isOrderHead = await _mMOnlineRepositoryWrapper.OrderHeadRepository
                                                           .GetQuery()
                                                           .AsNoTracking()
                                                           .FirstOrDefaultAsync(a => a.OrderHeadId == model.ErpId);

            if (isOrderHead == null)
            {
                response.message = "ErpId kaydı sistemde bulunamadı!";
                response.success = false;
                response.code = 1002;
                return Result(response);
            }

            if (!model.ChannelCode.Equals(isOrderHead.ChannelCode))
            {

                response.message = "ChannelCode, ErpId kaydına ait ChannelCode ile uyuşmamaktadır!";
                response.success = false;
                response.code = 1006;
                return Result(response);
            }

            var isOrderItem = await _mMOnlineRepositoryWrapper.OrderItemRepository
                                                         .GetQuery()
                                                         .AsNoTracking()
                                                         .FirstOrDefaultAsync(a => a.OrderHeadId == model.ErpId);
            if (isOrderItem!=null)
            {
                if (model.Article != isOrderItem.Article)
                {

                    response.message = "Article, ErpId kaydına ait Article ile uyuşmamaktadır!";
                    response.success = false;
                    response.code = 1007;
                    return Result(response);
                }
            }
            else
            {
                response.message = "ErpId kaydı sistemde bulunamadı!";
                response.success = false;
                response.code = 1002;
                return Result(response);

            }

            var key = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderStatusSettings.MarketPlaceKey + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var redisCategory = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderStatusSettings.RedisCategory;
            var order_DBUpdateData = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderStatusSettings.Order_DBUpdateData;
            var return_DBUpdateData = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderStatusSettings.Return_DBUpdateData;

            if (model.StatusType.Equals("order"))
            {
                var redisModel = new UpdateOrderStatusRedisModel()
                {
                    UpdateOrderStatusRequestModel = model,
                    Key = key,
                    CreatedDate = DateTime.Now
                };
                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<UpdateOrderStatusRedisModel>(redisCategory, order_DBUpdateData, redisModel, key, 1);
                if (!isSetRedisData)
                {
                    response.message = "Redis bağlantısı kurulamadı!";
                    response.success = false;
                    response.code = 1003;
                    return Result(response);
                }
            }
            else if (model.StatusType.Equals("return"))
            {
                var redisModel = new UpdateOrderStatusRedisModel()
                {
                    UpdateOrderStatusRequestModel = model,
                    Key = key,
                    CreatedDate = DateTime.Now
                };
                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<UpdateOrderStatusRedisModel>(redisCategory, return_DBUpdateData, redisModel, key, 11);
                if (!isSetRedisData)
                {
                    response.message = "Redis bağlantısı kurulamadı!";
                    response.success = false;
                    response.code = 1003;
                    return Result(response);
                }
            }
            else
            {
                response.message = "StatusType order veya return olmalıdır!";
                response.success = false;
                response.code = 1004;
                return Result(response);
            }

            var dataUpdateOrder = new DataUpdateOrder() { OrderHeadId = model.ErpId };
            response.message = "Başarılı!";
            response.success = true;
            response.code = 200;
            response.data = dataUpdateOrder;

            return Result(response);
        }
        catch (Exception ex)
        {
            response.message = "Sorgu sırasında hata oluştu!";
            response.success = false;
            response.code = 1005;
            return Result(response);

        }

    }
    public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, decimal>>>> GetVatRateBySearchAsync(string search)
    {
        var entities = await _mMOnlineRepositoryWrapper.VatKeyRepository
            .GetQuery()
            .AsNoTracking()
            .Where(a => a.VatKey == search)
            .Select(p => new KeyValueModel<string, decimal>
            {
                Key = p.VatKey,
                Value = (decimal)p.MpVatRate
            })
            .ToListAsync();

        return Result<IEnumerable<KeyValueModel<string, decimal>>>(entities);
    }
    public async Task<ServiceResultModel<IEnumerable<KeyValueModel<string, decimal>>>> GetVatRateByVatKeyAsync(List<int> vatKeys)
    {
        var entities = await _mMOnlineRepositoryWrapper.VatKeyRepository
            .GetQuery()
            .AsNoTracking()
            .Where(a => vatKeys.Contains(Convert.ToInt32(a.VatKey)))
            .Select(p => new KeyValueModel<string, decimal>
            {
                Key = p.VatKey,
                Value = (decimal)p.MpVatRate
            })
            .ToListAsync();

        return Result<IEnumerable<KeyValueModel<string, decimal>>>(entities);
    }
    public async Task<ServiceResultModel<OrderCancellationResponseModel>> OrderCancellationAsync(OrderCancellationRequestModel model)
    {
        var response = new OrderCancellationResponseModel();
        try
        {
            var isOrderHead = await _mMOnlineRepositoryWrapper.OrderHeadRepository
                                                           .GetQuery()
                                                           .AsNoTracking()
                                                           .FirstOrDefaultAsync(a => a.OrderHeadId == model.ErpId);

            if (isOrderHead == null)
            {
                response.message = "ErpId kaydı sistemde bulunamadı!";
                response.success = false;
                response.code = 1002;
                return Result(response);
            }

            if (!model.ChannelCode.Equals(isOrderHead.ChannelCode))
            {

                response.message = "ChannelCode, ErpId kaydına ait ChannelCode ile uyuşmamaktadır!";
                response.success = false;
                response.code = 1006;
                return Result(response);
            }

            var isOrderItem = await _mMOnlineRepositoryWrapper.OrderItemRepository
                                                         .GetQuery()
                                                         .AsNoTracking()
                                                         .FirstOrDefaultAsync(a => a.OrderHeadId == model.ErpId);
            if (isOrderItem != null)
            {
                if (model.Article != isOrderItem.Article)
                {

                    response.message = "Article, ErpId kaydına ait Article ile uyuşmamaktadır!";
                    response.success = false;
                    response.code = 1007;
                    return Result(response);
                }
            }
            else
            {
                response.message = "ErpId kaydı sistemde bulunamadı!";
                response.success = false;
                response.code = 1002;
                return Result(response);

            }
            var createdDate = DateTime.Now;          

            #region addDataToRedis

            var key = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderCancellationSettings.MarketPlaceKey + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var redisCategory = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderCancellationSettings.RedisCategory;
            var orderCancellation_NewData = _redisConfigModel.Value.MarketPlaceRedisSettings.OrderCancellationSettings.OrderCancellation_NewData;

            if (model != null)
            {
                model.RequestDate = createdDate;
                var redisModel = new OrderCancellationRequestRedisModel()
                {
                    OrderCancellationRequestModel = model,
                    Key = key,
                    CreatedDate = createdDate
                };
                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<OrderCancellationRequestRedisModel>(redisCategory, orderCancellation_NewData, redisModel, key, 0);
                if (!isSetRedisData)
                {
                    response.message = "Redis bağlantısı kurulamadı!";
                    response.success = false;
                    response.code = 1003;
                    return Result(response);
                }
            }

            #endregion

            var dataUpdateOrder = new DataUpdateOrder() { OrderHeadId = model.ErpId };
            response.message = "Başarılı!";
            response.success = true;
            response.code = 200;
            response.data = dataUpdateOrder;

            #region AddRawData

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            await _mmOnlineUnitOfWork.BeginTransactionAsync();

            var entity = new RawCancellationDataEntity();

            entity.RequestJson = JsonSerializer.Serialize(model, serializeOptions);
            entity.ResponseJson = JsonSerializer.Serialize(response, serializeOptions);
            entity.CreatedDate = createdDate;

            _mMOnlineRepositoryWrapper.RawCancellationDataRepository.Create(entity);

            await _mmOnlineUnitOfWork.SaveChangesAsync();
            await _mmOnlineUnitOfWork.CommitAsync();

            #endregion

            return Result(response);
        }
        catch (Exception ex)
        {
            await _mmOnlineUnitOfWork.RollbackAsync();
            response.message = "Sorgu sırasında hata oluştu!";
            response.success = false;
            response.code = 1005;
            return Result(response);

        }

    }

    #endregion
}