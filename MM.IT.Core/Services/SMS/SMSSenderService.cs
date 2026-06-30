using Dapper;
using IdentityModel.OidcClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Enums;
using MM.IT.Common.Extensions;
using MM.IT.Common.Helpers.SmsHelper;
using MM.IT.Common.Helpers.SmsHelper.Interfaces;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.MarketPlace;
using MM.IT.Common.Models.MMCustomerInfo;
using MM.IT.Common.Models.Sms;
using MM.IT.Common.Models.Sms.Redis;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using MM.IT.Core.Services.SMS.Interfaces;
using MM.IT.Data.Entities.MediaMarktIT;
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

namespace MM.IT.Core.Services.SMS
{
    public class SMSSenderService : BaseService, ISMSSenderService
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly ILogger<SMSSenderService> _logger;
        private readonly IMediaMarktITRepositoryWrapper _mediaMarktITRepositoryWrapper;
        private readonly ISmsHelper _smsHelper;
        private readonly IUnitOfWork<EFCoreMediaMarktITSqlProvider> _mediaMarktITUnitOfWork;
        private readonly IOptions<RedisConfigModel> _redisConfigModel;
        private readonly IRedisDistributedAdapter _redisDistributedAdapter;
        public SMSSenderService(IServiceProvider serviceProvider
            , IServiceWrapper serviceWrapper
            , ILogger<SMSSenderService> logger
            , IMediaMarktITRepositoryWrapper mediaMarktITRepositoryWrapper
            , ISmsHelper smsHelper
            , IUnitOfWork<EFCoreMediaMarktITSqlProvider> mediaMarktITUnitOfWork
            , IOptions<RedisConfigModel> redisConfigModel
            , IRedisDistributedAdapter redisDistributedAdapter) : base(serviceProvider)
        {
            _serviceWrapper = serviceWrapper;
            _logger = logger;
            _mediaMarktITRepositoryWrapper = mediaMarktITRepositoryWrapper;
            _smsHelper = smsHelper;
            _mediaMarktITUnitOfWork = mediaMarktITUnitOfWork;
            _redisConfigModel = redisConfigModel;
            _redisDistributedAdapter = redisDistributedAdapter;
        }
        public async Task<ServiceResultModel<SMSSenderResponse>> SMSSender(SMSSenderRequest model)
        {
            var sMSSenderResponse = new SMSSenderResponse();
            try
            {
                var secretData = "";
                var characterQuantity = 0;
                var smsQuantity = 0;

                #region checkModels

                if (string.IsNullOrEmpty(model.Mesgbody))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullMesgbody.GetDisplayName(), (int)MessageCodes.NotNullMesgbody);

                }
                if (!string.IsNullOrEmpty(model.Mesgbody))
                {
                    var trCharacterQuantity = GetTrChractersQuantity(model.Mesgbody);

                    characterQuantity = model.Mesgbody.Length + trCharacterQuantity;

                    if (characterQuantity > 894)
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.MessageLengthMesgbody.GetDisplayName(), (int)MessageCodes.MessageLengthMesgbody);

                    }

                    if (characterQuantity < 156)
                    {
                        smsQuantity = 1;
                    }
                    else
                    {
                        smsQuantity = GetSMSQuantity(characterQuantity);
                    }
                }

                if (!string.IsNullOrEmpty(model.MessageDescription))
                {
                    var charCount = model.MessageDescription.Length;
                    if (charCount > 50)
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.CharacterCountExceeded.GetDisplayName(), (int)MessageCodes.CharacterCountExceeded);

                    }
                }
                if (string.IsNullOrEmpty(model.Numbers))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullNumbers.GetDisplayName(), (int)MessageCodes.NotNullNumbers);

                }
                model.Numbers = model.Numbers.ReplaceAll(new[] { "(", ")", " ", "  " }, "").Trim();

                if (model.Numbers.Length < 9)
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.PhoneCharackterLess.GetDisplayName(), (int)MessageCodes.PhoneCharackterLess);

                }
                if (model.Numbers.Length > 10)
                {
                    model.Numbers = model.Numbers.Substring(model.Numbers.Length - 11);
                }
                if (string.IsNullOrEmpty(model.ChannelCode))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullChannelCode.GetDisplayName(), (int)MessageCodes.NotNullChannelCode);
                }
                var isChannel = await _mediaMarktITRepositoryWrapper.SMSChannelRepository
                                   .GetQuery()
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(a => a.ChannelCode == model.ChannelCode.Trim());

                if (isChannel == null)
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NothingChannelName.GetDisplayName(), (int)MessageCodes.NothingChannelName);

                }

                if (string.IsNullOrEmpty(model.SDate.ToString()))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullSDate.GetDisplayName(), (int)MessageCodes.NotNullSDate);
                }

                if (!CheckDateTimeFormat(model.SDate.ToString()))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.WrongFormatTheSDate.GetDisplayName(), (int)MessageCodes.WrongFormatTheSDate);
                }

                var difMinutes = GetTimeValue(Convert.ToDateTime(model.SDate), DateTime.Now);
                if (difMinutes < -60)
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.OldSDate.GetDisplayName(), (int)MessageCodes.OldSDate);

                }
                if (!string.IsNullOrEmpty(model.EDate.ToString()))
                {
                    if (!CheckDateTimeFormat(model.EDate.ToString()))
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.WrongFormatTheEDate.GetDisplayName(), (int)MessageCodes.WrongFormatTheEDate);

                    }
                    if (Convert.ToDateTime(model.EDate) < Convert.ToDateTime(model.SDate))
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.EDateCannotBeLessSDate.GetDisplayName(), (int)MessageCodes.EDateCannotBeLessSDate);

                    }
                    var startDate = Convert.ToDateTime(model.SDate);
                    var endDate = Convert.ToDateTime(model.EDate);
                    var difHours = (endDate - startDate).TotalHours;
                    //var difEDateAndSDate = GetTimeValue(endDate, startDate);
                    if (difHours > 72)
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.NoMoreThan72HoursBetweenEDateAndSDate.GetDisplayName(), (int)MessageCodes.NoMoreThan72HoursBetweenEDateAndSDate);

                    }
                }
                if (string.IsNullOrEmpty(model.MessageType))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullMessageType.GetDisplayName(), (int)MessageCodes.NotNullMessageType);

                }

                var messageType = model.MessageType.Trim().ToUpper();
                if (messageType != "N")
                {
                    if (messageType != "C")
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.InvalidMessageType.GetDisplayName(), (int)MessageCodes.InvalidMessageType);

                    }
                }
                if (model.MessageType.ToUpper().Equals("C") && string.IsNullOrEmpty(model.RecipientType))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.NotNullRecipientType.GetDisplayName(), (int)MessageCodes.NotNullRecipientType);

                }
                if (!string.IsNullOrEmpty(model.RecipientType))
                {
                    var recipientType = model.RecipientType.ToUpper();

                    if (recipientType != "B")
                    {
                        if (recipientType != "T")
                        {
                            return Result<SMSSenderResponse>(null, MessageCodes.InvalidRecipientType.GetDisplayName(), (int)MessageCodes.InvalidRecipientType);

                        }
                    }

                }

                var smsSendingCount = 3000;
                if (model.Numbers.Split(',').Count() > Convert.ToInt32(smsSendingCount))
                {
                    return Result<SMSSenderResponse>(null, MessageCodes.LimitExceededOfNumbers.GetDisplayName(), (int)MessageCodes.LimitExceededOfNumbers);

                }

                if (!string.IsNullOrEmpty(model.UserInfo))
                {
                    var userInfoLength = model.UserInfo.Length;
                    if (userInfoLength > 30)
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.MessageLengthUserInfo.GetDisplayName(), (int)MessageCodes.MessageLengthUserInfo);

                    }
                }

                if (!string.IsNullOrEmpty(model.OrderNumber))
                {

                    var orderNumberLength = model.OrderNumber.Length;
                    if (orderNumberLength > 10 || orderNumberLength < 7)
                    {
                        return Result<SMSSenderResponse>(null, MessageCodes.OrderNumberLengthMesgbody.GetDisplayName(), (int)MessageCodes.OrderNumberLengthMesgbody);

                    }
                }
                #endregion

                #region checkSecretData

                if (model.IsSecretData != null)
                {
                    if (model.IsSecretData)
                    {
                        var sMSEncodeKey = "b14ca5898a4e4133bbce2ea2315a1916";
                        secretData = SmsAESHelper.SifreleAES(model.Mesgbody, sMSEncodeKey);
                    }
                }

                #endregion

                var requestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var id = await _smsHelper.SendMobilDevSingleSMS(model);

                try
                {
                    var isNumeric = IsNumeric(id);
                    if (isNumeric)
                    {
                        var timerID = Convert.ToInt32(id);
                        try
                        {
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSSuccessfullEntity = new SMSSuccessfullEntity();

                            sMSSuccessfullEntity.ProcessId = timerID.ToString();
                            sMSSuccessfullEntity.Status = true;
                            sMSSuccessfullEntity.CreatedDate = DateTime.Now;

                            _mediaMarktITRepositoryWrapper.SMSSuccessfullRepository.Create(sMSSuccessfullEntity);

                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();

                            var SMSSuccessfullID = sMSSuccessfullEntity.ID;

                            #region addSmsHead

                            var numbers = model.Numbers.Split(',');

                            List<SMSSingleModel> sMSSingleModels = new List<SMSSingleModel>();

                            foreach (var item in numbers)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    SMSSingleModel sMSSingleModel = new SMSSingleModel();

                                    sMSSingleModel.SMSSuccessfullID = SMSSuccessfullID;
                                    sMSSingleModel.ChannelId = isChannel.ID;
                                    sMSSingleModel.SendType = nameof(SendType.Single);
                                    sMSSingleModel.Numbers = item;
                                    sMSSingleModel.Mesgbody = model.Mesgbody;
                                    if (!string.IsNullOrEmpty(secretData))
                                    {
                                        sMSSingleModel.Mesgbody = secretData;
                                        sMSSingleModel.IsSecretData = model.IsSecretData;
                                    }
                                    sMSSingleModel.MessageDescription = model.MessageDescription;
                                    sMSSingleModel.SDate = Convert.ToDateTime(model.SDate);
                                    if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
                                    {
                                        sMSSingleModel.EDate = Convert.ToDateTime(model.EDate);
                                    }
                                    sMSSingleModel.MessageType = model.MessageType;
                                    sMSSingleModel.RecipientType = model.RecipientType;
                                    sMSSingleModel.SMSStatus = 1;
                                    sMSSingleModel.Info1 = model.Info1;
                                    sMSSingleModel.Info2 = model.Info2;
                                    sMSSingleModel.Info3 = model.Info3;
                                    sMSSingleModel.OrderNumber = model.OrderNumber;
                                    sMSSingleModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                    sMSSingleModel.CharacterQuantity = characterQuantity;
                                    sMSSingleModel.SMSQuantity = smsQuantity;
                                    sMSSingleModel.SMSType = "Bilgilendirme";
                                    sMSSingleModel.CreatedDate = DateTime.Now;

                                    sMSSingleModels.Add(sMSSingleModel);
                                }
                            }


                            #endregion

                            #region addredisdata

                            var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                            var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                            var SingleRawData = _redisConfigModel.Value.SMSRedisSettings.SingleRawData;

                            if (model != null)
                            {
                                var redisModel = new SMSSingleRedisModel()
                                {
                                    SMSSingleModels = sMSSingleModels,
                                    Key = key,
                                    CreatedDate = DateTime.Now
                                };
                                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSSingleRedisModel>(redisCategory, SingleRawData, redisModel, key, 0);

                                if (!isSetRedisData)
                                {
                                    return Result<SMSSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                                }
                            }
                            #endregion

                            sMSSenderResponse.ID = timerID;
                            sMSSenderResponse.ErrorCode = 0;
                            sMSSenderResponse.Status = true;

                            return Result(sMSSenderResponse);


                        }
                        catch (Exception ex)
                        {

                            await _mediaMarktITUnitOfWork.RollbackAsync();
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSErrorEntity = new SMSErrorEntity();

                            sMSErrorEntity.ActionName = "SMSSender";
                            sMSErrorEntity.SourceName = "Single";
                            sMSErrorEntity.ProcessName = "SEND_0";
                            sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                            sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                            sMSErrorEntity.RequestDate = requestDate;
                            sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                            sMSErrorEntity.CreatedDate = DateTime.Now;


                            _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);

                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();


                            return Result<SMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);
                        }

                    }
                    else
                    {
                        await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                        var sMSErrorEntity = new SMSErrorEntity();

                        sMSErrorEntity.ActionName = "SMSSender";
                        sMSErrorEntity.SourceName = "Single";
                        sMSErrorEntity.ProcessName = "SEND_1";
                        sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                        sMSErrorEntity.RequestDate = requestDate;
                        sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                        sMSErrorEntity.CreatedDate = DateTime.Now;


                        _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);

                        await _mediaMarktITUnitOfWork.SaveChangesAsync();
                        await _mediaMarktITUnitOfWork.CommitAsync();

                        int SMSErrorID = sMSErrorEntity.ID;

                        List<SMSSingleModel> sMSSingleModels = new List<SMSSingleModel>();

                        #region addSmsHead

                        var numbers = model.Numbers.Split(',');

                        foreach (var item in numbers)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                SMSSingleModel sMSSingleModel = new SMSSingleModel();

                                sMSSingleModel.ChannelId = isChannel.ID;
                                sMSSingleModel.SMSErrorID = SMSErrorID;
                                sMSSingleModel.SendType = nameof(SendType.Single);
                                sMSSingleModel.Numbers = item;
                                sMSSingleModel.Mesgbody = model.Mesgbody;
                                if (!string.IsNullOrEmpty(secretData))
                                {
                                    sMSSingleModel.Mesgbody = secretData;
                                    sMSSingleModel.IsSecretData = model.IsSecretData;
                                }
                                sMSSingleModel.MessageDescription = model.MessageDescription;
                                sMSSingleModel.SDate = Convert.ToDateTime(model.SDate);
                                if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
                                {
                                    sMSSingleModel.EDate = Convert.ToDateTime(model.EDate);
                                }
                                sMSSingleModel.MessageType = model.MessageType;
                                sMSSingleModel.RecipientType = model.RecipientType;
                                sMSSingleModel.SMSStatus = 0;
                                sMSSingleModel.Info1 = model.Info1;
                                sMSSingleModel.Info2 = model.Info2;
                                sMSSingleModel.Info3 = model.Info3;
                                sMSSingleModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                sMSSingleModel.OrderNumber = model.OrderNumber;
                                sMSSingleModel.CharacterQuantity = characterQuantity;
                                sMSSingleModel.SMSQuantity = smsQuantity;
                                sMSSingleModel.SMSType = "Bilgilendirme";
                                sMSSingleModel.CreatedDate = DateTime.Now;

                                sMSSingleModels.Add(sMSSingleModel);



                            }
                        }

                        #endregion

                        #region addredisdata

                        var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                        var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                        var singleRawData = _redisConfigModel.Value.SMSRedisSettings.SingleRawData;

                        if (model != null)
                        {
                            var redisModel = new SMSSingleRedisModel()
                            {
                                SMSSingleModels = sMSSingleModels,
                                Key = key,
                                CreatedDate = DateTime.Now
                            };
                            var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSSingleRedisModel>(redisCategory, singleRawData, redisModel, key, 0);

                            if (!isSetRedisData)
                            {
                                return Result<SMSSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                            }
                        }
                        #endregion

                        return Result<SMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                    }
                }
                catch (Exception ex)
                {
                    await _mediaMarktITUnitOfWork.RollbackAsync();

                    await _mediaMarktITUnitOfWork.BeginTransactionAsync();
                    var entity = new SMSErrorEntity();

                    entity.ActionName = "SMSSender";
                    entity.SourceName = "Single";
                    entity.ProcessName = "SEND_2";
                    entity.ErrorMessage = ex.Message + ex.InnerException;
                    entity.RequestData = JsonConvert.SerializeObject(model);
                    entity.RequestDate = requestDate;
                    entity.ResponseData = JsonConvert.SerializeObject(id);
                    entity.CreatedDate = DateTime.Now;

                    await _mediaMarktITUnitOfWork.SaveChangesAsync();
                    await _mediaMarktITUnitOfWork.CommitAsync();

                    return Result<SMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                }

            }
            catch (Exception ex)
            {
                await _mediaMarktITUnitOfWork.RollbackAsync();
                _logger.LogError($"<SMSSenderService>SMSSender:{ex.Message}</SMSSenderService>");
                return Result<SMSSenderResponse>(null, MessageCodes.UnknownError.GetDisplayName(), (int)MessageCodes.UnknownError);
            }
        }
        public async Task<ServiceResultModel<SMSMultiSenderResponse>> SMSMultiSender(SMSMultiSenderRequest model)
        {
            SMSMultiSenderResponse sMSMultiSenderResponse = new SMSMultiSenderResponse();
            try
            {
                var secretData = "";

                #region checkModel

                if (model.Messages == null)
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NotNullMesgbody.GetDisplayName(), (int)MessageCodes.NotNullMesgbody);
                }

                foreach (var item in model.Messages)
                {
                    if (!string.IsNullOrEmpty(item.Mesgbody))
                    {
                        var trCharacterQuantity = GetTrChractersQuantity(item.Mesgbody);

                        var characterQuantity = item.Mesgbody.Length + trCharacterQuantity;

                        if (characterQuantity > 894)
                        {
                            return Result<SMSMultiSenderResponse>(null, MessageCodes.MessageLengthMesgbody.GetDisplayName(), (int)MessageCodes.MessageLengthMesgbody);
                        }

                        item.Number = item.Number.ReplaceAll(new[] { "(", ")", " ", "  " }, "").Trim();
                        if (item.Number.Length < 9)
                        {
                            return Result<SMSMultiSenderResponse>(null, MessageCodes.PhoneCharackterLess.GetDisplayName(), (int)MessageCodes.PhoneCharackterLess);

                        }
                        if (item.Number.Length > 10)
                        {
                            item.Number = item.Number.Substring(item.Number.Length - 11);

                        }
                    }
                }

                if (!string.IsNullOrEmpty(model.MessageDescription))
                {
                    var charCount = model.MessageDescription.Length;
                    if (charCount > 50)
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.CharacterCountExceeded.GetDisplayName(), (int)MessageCodes.CharacterCountExceeded);
                    }
                }
                if (string.IsNullOrEmpty(model.ChannelCode))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NotNullChannelCode.GetDisplayName(), (int)MessageCodes.NotNullChannelCode);
                }
                var isChannel = await _mediaMarktITRepositoryWrapper.SMSChannelRepository
                                .GetQuery()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.ChannelCode == model.ChannelCode.Trim());
                if (isChannel == null)
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NothingChannelName.GetDisplayName(), (int)MessageCodes.NothingChannelName);


                }
                if (string.IsNullOrEmpty(model.SDate.ToString()))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NotNullSDate.GetDisplayName(), (int)MessageCodes.NotNullSDate);
                }
                if (!CheckDateTimeFormat(model.SDate.ToString()))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.WrongFormatTheSDate.GetDisplayName(), (int)MessageCodes.WrongFormatTheSDate);

                }

                var difMinutes = GetTimeValue(Convert.ToDateTime(model.SDate), DateTime.Now);
                if (difMinutes < -60)
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.OldSDate.GetDisplayName(), (int)MessageCodes.OldSDate);


                }
                if (!string.IsNullOrEmpty(model.EDate.ToString()))
                {
                    if (!CheckDateTimeFormat(model.EDate.ToString()))
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.WrongFormatTheEDate.GetDisplayName(), (int)MessageCodes.WrongFormatTheEDate);
                    }
                    if (Convert.ToDateTime(model.EDate) < Convert.ToDateTime(model.SDate))
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.EDateCannotBeLessSDate.GetDisplayName(), (int)MessageCodes.EDateCannotBeLessSDate);

                    }

                    var startDate = Convert.ToDateTime(model.SDate);
                    var endDate = Convert.ToDateTime(model.EDate);
                    var difHours = (endDate - startDate).TotalHours;

                    //var difEDateAndSDate = GetTimeValue(Convert.ToDateTime(model.EDate), Convert.ToDateTime(model.SDate));
                    if (difHours > 72)
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.NoMoreThan72HoursBetweenEDateAndSDate.GetDisplayName(), (int)MessageCodes.NoMoreThan72HoursBetweenEDateAndSDate);
                    }
                }
                if (string.IsNullOrEmpty(model.MessageType))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NotNullMessageType.GetDisplayName(), (int)MessageCodes.NotNullMessageType);

                }
                var smsSendingCount = 3000;
                if (model.Messages.Count() > Convert.ToInt32(smsSendingCount))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.LimitExceededOfNumbers.GetDisplayName(), (int)MessageCodes.LimitExceededOfNumbers);

                }
                var messageType = model.MessageType.Trim().ToUpper();
                if (messageType != "N")
                {
                    if (messageType != "C")
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.InvalidMessageType.GetDisplayName(), (int)MessageCodes.InvalidMessageType);

                    }
                }
                if (model.MessageType.ToUpper().Equals("C") && string.IsNullOrEmpty(model.RecipientType))
                {
                    return Result<SMSMultiSenderResponse>(null, MessageCodes.NotNullRecipientType.GetDisplayName(), (int)MessageCodes.NotNullRecipientType);

                }
                if (!string.IsNullOrEmpty(model.RecipientType))
                {
                    var recipientType = model.RecipientType.ToUpper();

                    if (recipientType != "B")
                    {
                        if (recipientType != "T")
                        {
                            return Result<SMSMultiSenderResponse>(null, MessageCodes.InvalidRecipientType.GetDisplayName(), (int)MessageCodes.InvalidRecipientType);

                        }
                    }

                }
                if (!string.IsNullOrEmpty(model.UserInfo))
                {
                    var userInfoLength = model.UserInfo.Length;
                    if (userInfoLength > 30)
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.MessageLengthUserInfo.GetDisplayName(), (int)MessageCodes.MessageLengthUserInfo);

                    }
                }

                if (!string.IsNullOrEmpty(model.OrderNumber))
                {

                    var orderNumberLength = model.OrderNumber.Length;
                    if (orderNumberLength > 10 || orderNumberLength < 7)
                    {
                        return Result<SMSMultiSenderResponse>(null, MessageCodes.OrderNumberLengthMesgbody.GetDisplayName(), (int)MessageCodes.OrderNumberLengthMesgbody);
                    }
                }
                #endregion

                var requestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var id = await _smsHelper.SendMobilDevMultiSMS(model);

                try
                {
                    var smsQuantity = 0;
                    var isNumeric = IsNumeric(id);
                    if (isNumeric)
                    {
                        try
                        {
                            var timerID = Convert.ToInt32(id);
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSSuccessfullEntity = new SMSSuccessfullEntity();

                            sMSSuccessfullEntity.ProcessId = timerID.ToString();
                            sMSSuccessfullEntity.Status = true;
                            sMSSuccessfullEntity.CreatedDate = DateTime.Now;

                            _mediaMarktITRepositoryWrapper.SMSSuccessfullRepository.Create(sMSSuccessfullEntity);

                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();

                            var SMSSuccessfullID = sMSSuccessfullEntity.ID;

                            #region addSmsHead

                            List<SMSMultiModel> sMSMultiModels = new List<SMSMultiModel>();
                            foreach (var item in model.Messages)
                            {
                                if (!string.IsNullOrEmpty(item.Number))
                                {
                                    var trCharacterQuantity = GetTrChractersQuantity(item.Mesgbody);
                                    var characterQuantity = item.Mesgbody.Length + trCharacterQuantity;
                                    if (characterQuantity < 156)
                                    {
                                        smsQuantity = 1;
                                    }
                                    else
                                    {
                                        smsQuantity = GetSMSQuantity(characterQuantity);
                                    }

                                    SMSMultiModel sMSMultiModel = new SMSMultiModel();

                                    sMSMultiModel.ChannelId = isChannel.ID;
                                    sMSMultiModel.SMSSuccessfullID = SMSSuccessfullID;
                                    sMSMultiModel.SendType = nameof(SendType.Multi);
                                    sMSMultiModel.Numbers = item.Number.ToString();

                                    sMSMultiModel.Mesgbody = item.Mesgbody;

                                    #region checkSecretData

                                    if (model.IsSecretData != null)
                                    {
                                        if (model.IsSecretData)
                                        {
                                            var sMSEncodeKey = "b14ca5898a4e4133bbce2ea2315a1916";
                                            secretData = SmsAESHelper.SifreleAES(item.Mesgbody, sMSEncodeKey);

                                            sMSMultiModel.Mesgbody = secretData;
                                            sMSMultiModel.IsSecretData = true;
                                        }
                                    }

                                    #endregion

                                    sMSMultiModel.MessageDescription = model.MessageDescription;
                                    sMSMultiModel.SDate = Convert.ToDateTime(model.SDate);
                                    if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
                                    {
                                        sMSMultiModel.EDate = Convert.ToDateTime(model.EDate);
                                    }
                                    sMSMultiModel.MessageType = model.MessageType;
                                    sMSMultiModel.RecipientType = model.RecipientType;
                                    sMSMultiModel.SMSStatus = 1;
                                    sMSMultiModel.Info1 = model.Info1;
                                    sMSMultiModel.Info2 = model.Info2;
                                    sMSMultiModel.Info3 = model.Info3;
                                    sMSMultiModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                    sMSMultiModel.OrderNumber = model.OrderNumber;
                                    sMSMultiModel.CharacterQuantity = characterQuantity;
                                    sMSMultiModel.SMSQuantity = smsQuantity;
                                    sMSMultiModel.SMSType = "Bilgilendirme";
                                    sMSMultiModel.CreatedDate = DateTime.Now;


                                    sMSMultiModels.Add(sMSMultiModel);
                                }
                            }

                            #endregion

                            #region addredisdata

                            var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                            var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                            var multiRawData = _redisConfigModel.Value.SMSRedisSettings.MultiRawData;

                            if (model != null)
                            {
                                var redisModel = new SMSMultiRedisModel()
                                {
                                    SMSMultiModels = sMSMultiModels,
                                    Key = key,
                                    CreatedDate = DateTime.Now
                                };
                                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSMultiRedisModel>(redisCategory, multiRawData, redisModel, key, 0);

                                if (!isSetRedisData)
                                {
                                    return Result<SMSMultiSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                                }
                            }
                            #endregion

                            sMSMultiSenderResponse.ID = timerID;
                            sMSMultiSenderResponse.ErrorCode = 0;
                            sMSMultiSenderResponse.Status = true;

                            return Result(sMSMultiSenderResponse);



                        }
                        catch (Exception ex)
                        {
                            await _mediaMarktITUnitOfWork.RollbackAsync();

                            _logger.LogError($"<SMSSenderService>SMSMultiSender:SEND_4:{ex.Message}</SMSSenderService>");

                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSErrorEntity = new SMSErrorEntity();

                            sMSErrorEntity.ActionName = "SMSSender";
                            sMSErrorEntity.SourceName = "Multi";
                            sMSErrorEntity.ProcessName = "SEND_4";
                            sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                            sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                            sMSErrorEntity.RequestDate = requestDate;
                            sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);

                            _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);
                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();
                        }
                    }
                    else
                    {
                        try
                        {
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSErrorEntity = new SMSErrorEntity();

                            sMSErrorEntity.ActionName = "SMSSender";
                            sMSErrorEntity.SourceName = "Multi";
                            sMSErrorEntity.ProcessName = "SEND_5";
                            sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                            sMSErrorEntity.RequestDate = requestDate;
                            sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                            sMSErrorEntity.CreatedDate = DateTime.Now;

                            _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);
                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();

                            int SMSErrorID = sMSErrorEntity.ID;

                            #region addSmsHead

                            List<SMSMultiModel> sMSMultiModels = new List<SMSMultiModel>();
                            foreach (var item in model.Messages)
                            {

                                if (!string.IsNullOrEmpty(item.Number))
                                {
                                    SMSMultiModel sMSMultiModel = new SMSMultiModel();

                                    var trCharacterQuantity = GetTrChractersQuantity(item.Mesgbody);
                                    var characterQuantity = item.Mesgbody.Length + trCharacterQuantity;


                                    sMSMultiModel.ChannelId = isChannel.ID;
                                    sMSMultiModel.SMSErrorID = SMSErrorID;
                                    sMSMultiModel.SendType = nameof(SendType.Multi);
                                    sMSMultiModel.Numbers = item.Number.ToString();
                                    sMSMultiModel.Mesgbody = item.Mesgbody;

                                    #region checkSecretData

                                    if (model.IsSecretData != null)
                                    {
                                        if (model.IsSecretData)
                                        {
                                            var sMSEncodeKey = "b14ca5898a4e4133bbce2ea2315a1916";
                                            secretData = SmsAESHelper.SifreleAES(item.Mesgbody, sMSEncodeKey);

                                            sMSMultiModel.Mesgbody = secretData;
                                            sMSMultiModel.IsSecretData = true;
                                        }
                                    }

                                    #endregion

                                    sMSMultiModel.MessageDescription = model.MessageDescription;
                                    sMSMultiModel.SDate = Convert.ToDateTime(model.SDate);
                                    if (!string.IsNullOrEmpty(model.EDate) && CheckDateTimeFormat(model.EDate.ToString()))
                                    {
                                        sMSMultiModel.EDate = Convert.ToDateTime(model.EDate);
                                    }
                                    sMSMultiModel.MessageType = model.MessageType;
                                    sMSMultiModel.RecipientType = model.RecipientType;
                                    sMSMultiModel.SMSStatus = 0;
                                    sMSMultiModel.Info1 = model.Info1;
                                    sMSMultiModel.Info2 = model.Info2;
                                    sMSMultiModel.Info3 = model.Info3;
                                    sMSMultiModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                    sMSMultiModel.OrderNumber = model.OrderNumber;
                                    sMSMultiModel.CharacterQuantity = characterQuantity;
                                    sMSMultiModel.SMSQuantity = smsQuantity;
                                    sMSMultiModel.SMSType = "Bilgilendirme";
                                    sMSMultiModel.CreatedDate = DateTime.Now;

                                    sMSMultiModels.Add(sMSMultiModel);
                                }
                            }
                            #endregion

                            #region addredisdata

                            var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                            var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                            var multiRawData = _redisConfigModel.Value.SMSRedisSettings.MultiRawData;

                            if (model != null)
                            {
                                var redisModel = new SMSMultiRedisModel()
                                {
                                    SMSMultiModels = sMSMultiModels,
                                    Key = key,
                                    CreatedDate = DateTime.Now
                                };
                                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSMultiRedisModel>(redisCategory, multiRawData, redisModel, key, 0);

                                if (!isSetRedisData)
                                {
                                    return Result<SMSMultiSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                                }
                            }
                            #endregion

                            return Result<SMSMultiSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                        }
                        catch (Exception ex)
                        {
                            await _mediaMarktITUnitOfWork.RollbackAsync();

                            _logger.LogError($"<SMSSenderService>SMSMultiSender:SEND_6:{ex.Message}</SMSSenderService>");

                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSErrorEntity = new SMSErrorEntity();

                            sMSErrorEntity.ActionName = "SMSSender";
                            sMSErrorEntity.SourceName = "Multi";
                            sMSErrorEntity.ProcessName = "SEND_6";
                            sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                            sMSErrorEntity.RequestDate = requestDate;
                            sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                            sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                            sMSErrorEntity.CreatedDate = DateTime.Now;


                            _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);
                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();

                            return Result<SMSMultiSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                        }
                    }
                }
                catch (Exception ex)
                {
                    await _mediaMarktITUnitOfWork.RollbackAsync();

                    _logger.LogError($"<SMSSenderService>SMSMultiSender:SEND_7:{ex.Message}</SMSSenderService>");
                    await _mediaMarktITUnitOfWork.BeginTransactionAsync();
                    var sMSErrorEntity = new SMSErrorEntity();

                    sMSErrorEntity.ActionName = "SMSSender";
                    sMSErrorEntity.SourceName = "Multi";
                    sMSErrorEntity.ProcessName = "SEND_7";
                    sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                    sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                    sMSErrorEntity.RequestDate = requestDate;
                    sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                    sMSErrorEntity.CreatedDate = DateTime.Now;

                    _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);
                    await _mediaMarktITUnitOfWork.SaveChangesAsync();
                    await _mediaMarktITUnitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                await _mediaMarktITUnitOfWork.RollbackAsync();

                _logger.LogError($"<SMSSenderService>SMSMultiSender:SEND_8:{ex.Message}</SMSSenderService>");

                await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                var sMSErrorEntity = new SMSErrorEntity();

                sMSErrorEntity.ActionName = "SMSSender";
                sMSErrorEntity.SourceName = "Multi";
                sMSErrorEntity.ProcessName = "SEND_8";
                sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                sMSErrorEntity.CreatedDate = DateTime.Now;

                _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);
                await _mediaMarktITUnitOfWork.SaveChangesAsync();
                await _mediaMarktITUnitOfWork.CommitAsync();

                return Result<SMSMultiSenderResponse>(null, MessageCodes.UnknownError.GetDisplayName(), (int)MessageCodes.UnknownError);
            }
            return Result<SMSMultiSenderResponse>(null, MessageCodes.UnknownError.GetDisplayName(), (int)MessageCodes.UnknownError);

        }
        public async Task<ServiceResultModel<OTPSMSSenderResponse>> OTPSMSSender(OTPSMSSenderRequest model)
        {
            var oTPSMSSenderResponse = new OTPSMSSenderResponse();
            try
            {
                var secretData = "";
                var characterQuantity = 0;
                var smsQuantity = 0;

                #region checkModels

                if (string.IsNullOrEmpty(model.Mesgbody))
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.NotNullMesgbody.GetDisplayName(), (int)MessageCodes.NotNullMesgbody);

                }
                if (!string.IsNullOrEmpty(model.Mesgbody))
                {
                    var trCharacterQuantity = GetTrChractersQuantity(model.Mesgbody);

                    characterQuantity = model.Mesgbody.Length + trCharacterQuantity;

                    if (characterQuantity > 894)
                    {
                        return Result<OTPSMSSenderResponse>(null, MessageCodes.MessageLengthMesgbody.GetDisplayName(), (int)MessageCodes.MessageLengthMesgbody);

                    }

                    if (characterQuantity < 156)
                    {
                        smsQuantity = 1;
                    }
                    else
                    {
                        smsQuantity = GetSMSQuantity(characterQuantity);
                    }
                }

                if (!string.IsNullOrEmpty(model.MessageDescription))
                {
                    var charCount = model.MessageDescription.Length;
                    if (charCount > 50)
                    {
                        return Result<OTPSMSSenderResponse>(null, MessageCodes.CharacterCountExceeded.GetDisplayName(), (int)MessageCodes.CharacterCountExceeded);

                    }
                }
                if (string.IsNullOrEmpty(model.Numbers))
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.NotNullNumbers.GetDisplayName(), (int)MessageCodes.NotNullNumbers);

                }
                model.Numbers = model.Numbers.ReplaceAll(new[] { "(", ")", " ", "  " }, "").Trim();

                if (model.Numbers.Trim().Length < 9)
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.PhoneCharackterLess.GetDisplayName(), (int)MessageCodes.PhoneCharackterLess);

                }

                if (model.Numbers.Length > 10)
                {
                    model.Numbers = model.Numbers.Substring(model.Numbers.Length - 11);

                }

                if (string.IsNullOrEmpty(model.ChannelCode))
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.NotNullChannelCode.GetDisplayName(), (int)MessageCodes.NotNullChannelCode);
                }
                var isChannel = await _mediaMarktITRepositoryWrapper.SMSChannelRepository
                                   .GetQuery()
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(a => a.ChannelCode == model.ChannelCode.Trim());

                if (isChannel == null)
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.NothingChannelName.GetDisplayName(), (int)MessageCodes.NothingChannelName);

                }

                var smsSendingCount = 3000;
                if (model.Numbers.Split(',').Count() > Convert.ToInt32(smsSendingCount))
                {
                    return Result<OTPSMSSenderResponse>(null, MessageCodes.LimitExceededOfNumbers.GetDisplayName(), (int)MessageCodes.LimitExceededOfNumbers);

                }

                if (!string.IsNullOrEmpty(model.UserInfo))
                {
                    var userInfoLength = model.UserInfo.Length;
                    if (userInfoLength > 30)
                    {
                        return Result<OTPSMSSenderResponse>(null, MessageCodes.MessageLengthUserInfo.GetDisplayName(), (int)MessageCodes.MessageLengthUserInfo);

                    }
                }

                if (!string.IsNullOrEmpty(model.OrderNumber))
                {

                    var orderNumberLength = model.OrderNumber.Length;
                    if (orderNumberLength > 10 || orderNumberLength < 7)
                    {
                        return Result<OTPSMSSenderResponse>(null, MessageCodes.OrderNumberLengthMesgbody.GetDisplayName(), (int)MessageCodes.OrderNumberLengthMesgbody);

                    }
                }
                #endregion

                #region checkSecretData

                if (model.IsSecretData != null)
                {
                    if (model.IsSecretData)
                    {
                        var sMSEncodeKey = "b14ca5898a4e4133bbce2ea2315a1916";
                        secretData = SmsAESHelper.SifreleAES(model.Mesgbody, sMSEncodeKey);
                    }
                }

                #endregion

                var requestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var id = await _smsHelper.SendMobilDevOTPSMS(model);

                try
                {
                    var isNumeric = IsNumeric(id);
                    if (isNumeric)
                    {
                        var timerID = Convert.ToInt32(id);
                        try
                        {
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSSuccessfullEntity = new SMSSuccessfullEntity();

                            sMSSuccessfullEntity.ProcessId = timerID.ToString();
                            sMSSuccessfullEntity.Status = true;
                            sMSSuccessfullEntity.CreatedDate = DateTime.Now;

                            _mediaMarktITRepositoryWrapper.SMSSuccessfullRepository.Create(sMSSuccessfullEntity);

                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();

                            var SMSSuccessfullID = sMSSuccessfullEntity.ID;

                            #region addSmsHead

                            var numbers = model.Numbers.Split(',');

                            List<SMSSingleModel> sMSSingleModels = new List<SMSSingleModel>();

                            foreach (var item in numbers)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    SMSSingleModel sMSSingleModel = new SMSSingleModel();

                                    sMSSingleModel.SMSSuccessfullID = SMSSuccessfullID;
                                    sMSSingleModel.ChannelId = isChannel.ID;
                                    sMSSingleModel.SendType = nameof(SendType.Single);
                                    sMSSingleModel.Numbers = item;
                                    sMSSingleModel.MessageType = "OTP";
                                    sMSSingleModel.Mesgbody = model.Mesgbody;
                                    if (!string.IsNullOrEmpty(secretData))
                                    {
                                        sMSSingleModel.Mesgbody = secretData;
                                        sMSSingleModel.IsSecretData = model.IsSecretData;
                                    }
                                    sMSSingleModel.MessageDescription = model.MessageDescription;
                                    sMSSingleModel.SDate = DateTime.Now;
                                    sMSSingleModel.SMSStatus = 1;
                                    sMSSingleModel.Info1 = model.Info1;
                                    sMSSingleModel.Info2 = model.Info2;
                                    sMSSingleModel.Info3 = model.Info3;
                                    sMSSingleModel.OrderNumber = model.OrderNumber;
                                    sMSSingleModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                    sMSSingleModel.CharacterQuantity = characterQuantity;
                                    sMSSingleModel.SMSQuantity = smsQuantity;
                                    sMSSingleModel.SMSType = "OTP";
                                    sMSSingleModel.CreatedDate = DateTime.Now;

                                    sMSSingleModels.Add(sMSSingleModel);
                                }
                            }


                            #endregion

                            #region addredisdata

                            var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                            var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                            var oTPRawData = _redisConfigModel.Value.SMSRedisSettings.OTPRawData;

                            if (model != null)
                            {
                                var redisModel = new SMSSingleRedisModel()
                                {
                                    SMSSingleModels = sMSSingleModels,
                                    Key = key,
                                    CreatedDate = DateTime.Now
                                };
                                var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSSingleRedisModel>(redisCategory, oTPRawData, redisModel, key, 0);

                                if (!isSetRedisData)
                                {
                                    return Result<OTPSMSSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                                }
                            }
                            #endregion

                            oTPSMSSenderResponse.ID = timerID;
                            oTPSMSSenderResponse.ErrorCode = 0;
                            oTPSMSSenderResponse.Status = true;

                            return Result(oTPSMSSenderResponse);


                        }
                        catch (Exception ex)
                        {

                            await _mediaMarktITUnitOfWork.RollbackAsync();
                            await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                            var sMSErrorEntity = new SMSErrorEntity();

                            sMSErrorEntity.ActionName = "SMSSender";
                            sMSErrorEntity.SourceName = "Single";
                            sMSErrorEntity.ProcessName = "SEND_0";
                            sMSErrorEntity.ErrorMessage = ex.Message + ex.InnerException;
                            sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                            sMSErrorEntity.RequestDate = requestDate;
                            sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                            sMSErrorEntity.CreatedDate = DateTime.Now;


                            _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);

                            await _mediaMarktITUnitOfWork.SaveChangesAsync();
                            await _mediaMarktITUnitOfWork.CommitAsync();


                            return Result<OTPSMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);
                        }

                    }
                    else
                    {
                        await _mediaMarktITUnitOfWork.BeginTransactionAsync();

                        var sMSErrorEntity = new SMSErrorEntity();

                        sMSErrorEntity.ActionName = "SMSSender";
                        sMSErrorEntity.SourceName = "Single";
                        sMSErrorEntity.ProcessName = "SEND_1";
                        sMSErrorEntity.RequestData = JsonConvert.SerializeObject(model);
                        sMSErrorEntity.RequestDate = requestDate;
                        sMSErrorEntity.ResponseData = JsonConvert.SerializeObject(id);
                        sMSErrorEntity.CreatedDate = DateTime.Now;


                        _mediaMarktITRepositoryWrapper.SMSErrorRepository.Create(sMSErrorEntity);

                        await _mediaMarktITUnitOfWork.SaveChangesAsync();
                        await _mediaMarktITUnitOfWork.CommitAsync();

                        int SMSErrorID = sMSErrorEntity.ID;

                        List<SMSSingleModel> sMSSingleModels = new List<SMSSingleModel>();

                        #region addSmsHead

                        var numbers = model.Numbers.Split(',');

                        foreach (var item in numbers)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                SMSSingleModel sMSSingleModel = new SMSSingleModel();

                                sMSSingleModel.ChannelId = isChannel.ID;
                                sMSSingleModel.SMSErrorID = SMSErrorID;
                                sMSSingleModel.SendType = nameof(SendType.Single);
                                sMSSingleModel.Numbers = item;
                                sMSSingleModel.MessageType = "OTP";
                                sMSSingleModel.Mesgbody = model.Mesgbody;
                                if (!string.IsNullOrEmpty(secretData))
                                {
                                    sMSSingleModel.Mesgbody = secretData;
                                    sMSSingleModel.IsSecretData = model.IsSecretData;
                                }
                                sMSSingleModel.MessageDescription = model.MessageDescription;
                                sMSSingleModel.SDate = DateTime.Now;
                                sMSSingleModel.SMSStatus = 0;
                                sMSSingleModel.Info1 = model.Info1;
                                sMSSingleModel.Info2 = model.Info2;
                                sMSSingleModel.Info3 = model.Info3;
                                sMSSingleModel.UserInfo = !string.IsNullOrEmpty(model.UserInfo) ? model.UserInfo : "";
                                sMSSingleModel.OrderNumber = model.OrderNumber;
                                sMSSingleModel.CharacterQuantity = characterQuantity;
                                sMSSingleModel.SMSQuantity = smsQuantity;
                                sMSSingleModel.SMSType = "OTP";
                                sMSSingleModel.CreatedDate = DateTime.Now;

                                sMSSingleModels.Add(sMSSingleModel);



                            }
                        }

                        #endregion

                        #region addredisdata

                        var key = _redisConfigModel.Value.SMSRedisSettings.RedisCategory + "_" + Guid.NewGuid();
                        var redisCategory = _redisConfigModel.Value.SMSRedisSettings.RedisCategory;
                        var oTPRawData = _redisConfigModel.Value.SMSRedisSettings.OTPRawData;

                        if (model != null)
                        {
                            var redisModel = new SMSSingleRedisModel()
                            {
                                SMSSingleModels = sMSSingleModels,
                                Key = key,
                                CreatedDate = DateTime.Now
                            };
                            var isSetRedisData = _redisDistributedAdapter.SetRedisDataWithKey<SMSSingleRedisModel>(redisCategory, oTPRawData, redisModel, key, 0);

                            if (!isSetRedisData)
                            {
                                return Result<OTPSMSSenderResponse>(null, "Redis bağlantısı kurulamadı!", 1027);

                            }
                        }
                        #endregion

                        return Result<OTPSMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                    }
                }
                catch (Exception ex)
                {
                    await _mediaMarktITUnitOfWork.RollbackAsync();

                    await _mediaMarktITUnitOfWork.BeginTransactionAsync();
                    var entity = new SMSErrorEntity();

                    entity.ActionName = "SMSSender";
                    entity.SourceName = "Single";
                    entity.ProcessName = "SEND_2";
                    entity.ErrorMessage = ex.Message + ex.InnerException;
                    entity.RequestData = JsonConvert.SerializeObject(model);
                    entity.RequestDate = requestDate;
                    entity.ResponseData = JsonConvert.SerializeObject(id);
                    entity.CreatedDate = DateTime.Now;

                    await _mediaMarktITUnitOfWork.SaveChangesAsync();
                    await _mediaMarktITUnitOfWork.CommitAsync();

                    return Result<OTPSMSSenderResponse>(null, MessageCodes.UnexpectedError.GetDisplayName(), (int)MessageCodes.UnexpectedError);

                }

            }
            catch (Exception ex)
            {
                await _mediaMarktITUnitOfWork.RollbackAsync();
                _logger.LogError($"<SMSSenderService>SMSSender:{ex.Message}</SMSSenderService>");
                return Result<OTPSMSSenderResponse>(null, MessageCodes.UnknownError.GetDisplayName(), (int)MessageCodes.UnknownError);
            }
        }

        #region PUBLIC_METHODS
        public bool IsNumeric(string inputString)
        {
            int n;
            bool isNumeric = int.TryParse(inputString, out n);

            return isNumeric;
        }
        public bool CheckDateTimeFormat(string inputString)
        {
            DateTime dDate;

            if (DateTime.TryParse(inputString, out dDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetTimeValue(DateTime endDate, DateTime startDate)
        {
            var dif = (int)(endDate - startDate).TotalHours;
            return dif;
        }
        public int GetTrChractersQuantity(string Message)
        {
            char[] trChars = { 'Ş', 'ş', 'ç', 'Ğ', 'ğ', 'İ', 'ı' };//["Ş", "ş", "ç", "Ğ", "ğ", "İ", "ı"];
            int adet = 0;
            foreach (char ch in Message)
            {
                if (ch == 'Ş')
                {
                    adet++;
                }
                else if (ch == 'ş')
                {
                    adet++;
                }
                else if (ch == 'ç')
                {
                    adet++;
                }
                else if (ch == 'Ğ')
                {
                    adet++;
                }
                else if (ch == 'ğ')
                {
                    adet++;
                }
                else if (ch == 'İ')
                {
                    adet++;
                }
                else if (ch == 'ı')
                {
                    adet++;
                }
            }
            return adet;
        }
        public int GetSMSQuantity(int CharacterQuantity)
        {

            int adet = 0;
            decimal bolum, value;

            bolum = CharacterQuantity / 149;

            if (Decimal.TryParse(bolum.ToString(), out value))
            {
                adet = Convert.ToInt32(bolum) + 1;
            }
            else
            {
                adet = Convert.ToInt32(bolum);
            }
            return adet;
        }

        #endregion
    }
}
