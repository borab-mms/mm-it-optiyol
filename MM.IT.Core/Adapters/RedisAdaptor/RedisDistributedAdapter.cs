using MailKit.Search;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Constants;
using MM.IT.Common.Helpers.RedisHelper;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.EKOLStock;
using MM.IT.Common.Models.MEX;
using MM.IT.Common.Models.Sterling;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanizer.In;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MM.IT.Core.Adapters.RedisAdaptor;

public class RedisDistributedAdapter : IRedisDistributedAdapter
{
    private readonly IOptions<RedisConfigModel> _redisConfigs;
    private readonly ILogger<RedisDistributedAdapter> _logger;
    public RedisDistributedAdapter(IOptions<RedisConfigModel> redisConfigs
        , ILogger<RedisDistributedAdapter> logger)
    {
        _redisConfigs = redisConfigs;
        _logger = logger;
    }
    public void RedisConnect()
    {
        RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
    }
    public bool IsRedisConnect()
    {
        var result = RedisHelper.IsRedisConnect();

        if (result)
        {
            return true;
        }
        return false;
    }

    #region setData
    public bool SetRedisData<T>(string applicationCode, string redisCategory, string toFolder, T model, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), state)
                       .RightPushKey(model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public async Task<bool> SetRedisDataAsync<T>(string applicationCode, string redisCategory, string toFolder, T model, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), state)
                       .RightPushKeyAsync(model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public bool SetRedisData(string applicationCode, string redisCategory, string rawDataKey, object model, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }
            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, rawDataKey), state).RightPushKey(model);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"<FOMJobs>SetRedisData:{ex.Message}-{ex.InnerException}</FOMJobs>");
            return false;
        }

    }
    public bool SetRedisDataWithKey<T>(string redisCategory, string rawDataKey, T model, string key, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            }
            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, rawDataKey), state)
                            .SetHashKeyValue(key, model);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public async Task<bool> SetRedisDataWithKeyAsync<T>(string redisCategory, string rawDataKey, T model, string key, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            }
            await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, rawDataKey), state)
                            .SetHashKeyValueAsync(key, model);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    #endregion

    #region getData
    public PaymentResponse GetRedisData(string redisCategory, string folderName, int state)
    {
        var response = new PaymentResponse();
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                                    .ListLeftPop<PaymentResponse>();
            if (listLeftPop != null)
            {
                response = listLeftPop;
            }
        }

        return response;
    }
    public T GetRedisData<T>(string redisCategory, string folderName, int state)
    {
        T data;
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                                    .ListLeftPop<T>();
            if (listLeftPop != null)
            {
                return listLeftPop;
            }
        }

        return default(T);
    }
    public List<PaymentResponse> GetRedisAllData(string redisCategory, string rawDataKey, int state)
    {
        var response = new List<PaymentResponse>();

        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, rawDataKey))
                                    .GetKeyValues<List<PaymentResponseTest>>();
            if (listLeftPop != null)
            {
                var aa = listLeftPop.ToString();
            }
        }

        return response;
    }
    public PaymentResponse GetRedisRawData(string redisCategory, string rawDataKey)
    {
        var response = new PaymentResponse();
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, rawDataKey))
                                    .ListLeftPop<PaymentResponse>();
            if (listLeftPop != null)
            {
                response = listLeftPop;
            }
        }

        return response;
    }
    public List<T> GetRedisAllDataWithKeyOld<T>(string redisCategory, string folderName, int state)
    {
        var response = new List<T>();

        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            //MediaMarktIT:MarketPlaceOrderStatus:OrderCancel_EkolUpdateDate:6
            var getKeyValues = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                                    .GetKeyValues<T>();
            return getKeyValues;
        }

        return response;
    }
    public List<T> GetRedisAllDataWithKey<T>(string redisCategory, string folderName, int state)
    {
        var response = new List<T>();
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            }
            var getKeyValues = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                                       .GetKeyValues<T>();
            return getKeyValues;

        }
        catch (Exception)
        {

            return response;
        }
    }

    #endregion

    #region updateData
    public bool UpdateRedisState(string redisCategory, string folderName, int state, PaymentResponse paymentResponse)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            if (paymentResponse != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).RightPushKey(paymentResponse);
            }
        }

        return true;
    }
    public bool UpdateRedisState<T>(string redisCategory, string folderName, int state, T model)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            if (model != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).RightPushKey(model);
            }
        }

        return true;
    }
    public bool ChangeRedisState<T>(string redisCategory, string FromFolderName, string ToFolderName, int fromState, int toState)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, FromFolderName), fromState)
                               .ListLeftPop<T>();
            if (listLeftPop != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).RightPushKey(listLeftPop);
                return true;
            }

        }

        return false;
    }
    public bool UpdateRedisState<T>(string redisCategory, string FromFolderName, string ToFolderName, string key, int toState, int fromState = 0)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var getHashKeyValue = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, FromFolderName), fromState)
                                       .GetHashKeyValue<T>(key);
            if (getHashKeyValue != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetHashKeyValue(key, getHashKeyValue);
                return true;
            }
        }
        return false;
    }
    public bool UpdateRedisStateWithTimeAndKey<T>(string redisCategory, string FromFolderName, string ToFolderName, string key, int fromState, int toState, int hour = 0, int day = 0)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            var getHashKeyValue = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, FromFolderName), fromState)
                                       .GetHashKeyValue<T>(key);
            if (getHashKeyValue != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetHashKeyValue(key, getHashKeyValue);
                if (hour > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetExpire(TimeSpan.FromHours(hour));
                }
                else if (day > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetExpire(TimeSpan.FromDays(day));

                }
                return true;
            }
        }
        return false;
    }
    public bool UpdateRedisStateWithTime<T>(string redisCategory, string folderName, int state, T model, int hour = 0, int day = 0)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            if (model != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).RightPushKey(model);
                if (hour > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetExpire(TimeSpan.FromHours(hour));
                }
                else if (day > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetExpire(TimeSpan.FromDays(day));

                }
            }
        }

        return true;
    }
    public bool UpdateRedisStateWithTime(string redisCategory, string folderName, int state, PaymentResponse paymentResponse, int hour = 0, int day = 0)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            if (paymentResponse != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).RightPushKey(paymentResponse);
                if (hour > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetExpire(TimeSpan.FromHours(hour));
                }
                else if (day > 0)
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetExpire(TimeSpan.FromDays(day));

                }
            }
        }

        return true;
    }
    public bool UpdateRedisStateWithStringKey<T>(string redisCategory, string folderName, int state, T model, DateTime expireDate)
    {
        var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
        if (isRedisConnect)
        {
            if (model != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetStringKeyValue(model, expireDate);

            }
        }

        return true;
    }

    #endregion

    #region deleteData
    public bool DeleteRedisData(string redisCategory, string folderName, int state)
    {
        try
        {

            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).DeleteKey();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public bool DeleteRedisDataWithHashKeyOld(string redisCategory, string folderName, string key, int state)
    {
        try
        {

            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                       .DeleteHashKey(key);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public bool DeleteRedisDataWithHashKey(string redisCategory, string folderName, string key, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            }
            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                       .DeleteHashKey(key);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public async Task<bool> DeleteRedisDataWithHashKeyAsync(string redisCategory, string folderName, string key, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            }
            await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                       .DeleteHashKeyAsync(key);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    #endregion

    public bool AddOrUpdateRedisState<T>(string redisCategory, string folderName, T model, string key, int state)
    {
        try
        {

            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state).SetHashKeyValue(key, model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public T ListRightPopLeftPush<T>(string redisCategory, string FromFolderName, int fromState, int nextState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, FromFolderName), fromState)
                   .ListRightPopLeftPush<T>(nextState);
            if (listLeftPop != null)
            {
                return listLeftPop;
            }
        }
        catch (Exception)
        {
            return (T)new object();
        }

        return (T)new object();
    }
    public bool SetRedisKey<T>(string redisCategory, string ToFolderName, T model, string key, int toState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetHashKeyValue(key, model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public List<CustomerOrderRedisModel> GetRedisKeyByCreatedDate(string redisCategory, string folderName, DateTime dateTime, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var getKeyValues = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), state)
                                      .GetKeyValuesByCreatedDate(dateTime);
            return getKeyValues;

        }
        catch (Exception ex)
        {
            _logger.LogInformation($"<FOMJobs>GetRedisKeyByCreatedDate:{ex.Message}-{ex.InnerException}</FOMJobs>");
        }
        return null;
    }
    public bool SetAndDeleteRedisDataOld<T>(string redisCategory, string deleteFolder, int deleteState, string deleteKey, string toFolder, int state, T model)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var isRedisConnect = RedisHelper.RedisConnect(_redisConfigs.Value.Host, _redisConfigs.Value.Port, _redisConfigs.Value.Password);
            if (isRedisConnect)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), state)
                           .RightPushKey(model);
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, deleteFolder), deleteState)
                          .DeleteHashKey(deleteKey);

            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public bool SetAndDeleteRedisData<T>(string redisCategory, string deleteFolder, int deleteState, string deleteKey, string toFolder, int state, T model)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, deleteFolder), deleteState)
                       .ListLeftPop();
        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetAndDeleteRedisData:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }

        return true;
    }
    public bool ChangeRedisStateData<T>(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, string actionName = null, string orderNo = null)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }
            var data = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, fromFolder), fromState)
                       .ListLeftPop();
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Error))
                {
                    _logger.LogInformation($"<FOMJobs>ChangeRedisStateDataError:{data.Error}</FOMJobs>");
                    return false;

                }
                try
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), toState)
                              .RightPushKey(data.CustomerOrderRedisModel);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"<FOMJobs>ChangeRedisStateDataIn:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");

                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>ChangeRedisStateData:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");
        }

        return false;
    }
    public async Task<bool> ChangeRedisStateDataAsync<T>(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, string actionName = null, string orderNo = null)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }
            var data = await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, fromFolder), fromState)
                       .ListLeftPopAsync();
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Error))
                {
                    _logger.LogInformation($"<FOMJobs>ChangeRedisStateDataError:{data.Error}</FOMJobs>");
                    return false;

                }
                try
                {
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), toState)
                              .RightPushKey(data.CustomerOrderRedisModel);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"<FOMJobs>ChangeRedisStateDataIn:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");

                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>ChangeRedisStateData:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");
        }

        return false;
    }
    public bool ChangeModelData(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, CustomerOrderRedisModel model, string actionName = null, string orderNo = null)
    {
        try
        {

            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }
            var data = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, fromFolder), fromState)
                       .ListLeftPop();
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Error))
                {
                    _logger.LogInformation($"<FOMJobs>ChangeModelDataError:{data.Error}</FOMJobs>");
                    return false;

                }
                try
                {

                    data.CustomerOrderRedisModel.CustomerOrder = model.CustomerOrder;
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), toState)
                              .RightPushKey(data.CustomerOrderRedisModel);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"<FOMJobs>ChangeModelDataIn:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");

                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>ChangeModelData:{toState}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");
        }
        return false;
    }
    public async Task<bool> ChangeModelDataAsync(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, CustomerOrderRedisModel model, string actionName = null, string orderNo = null)
    {
        try
        {

            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }
            var data = await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, fromFolder), fromState)
                       .ListLeftPopAsync();
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Error))
                {
                    _logger.LogInformation($"<FOMJobs>ChangeModelDataError:{data.Error}</FOMJobs>");
                    return false;

                }
                try
                {

                    data.CustomerOrderRedisModel.CustomerOrder = model.CustomerOrder;
                    RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), toState)
                              .RightPushKey(data.CustomerOrderRedisModel);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"<FOMJobs>ChangeModelDataIn:{actionName}-{orderNo}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");

                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>ChangeModelData:{toState}-{fromState}-{fromFolder}-{fromFolder}-{ex.Message}-{ex.InnerException}</FOMJobs>");
        }
        return false;
    }
    public bool SetErrorData(string redisCategory, string deleteFolder, int deleteState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, deleteFolder), deleteState)
                       .ListLeftPop();

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }

        return true;
    }
    public async Task<bool> SetErrorDataAsync(string redisCategory, string deleteFolder, int deleteState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


           await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, deleteFolder), deleteState)
                       .ListLeftPopAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }

        return true;
    }
    public CustomerOrderRedisModel GetAndSetWaitingData(string redisCategory, string folderName, int fromState, string ToFolderName, int toState)
    {
        //TODO:connect kaldırılacak
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), fromState)
    .ListLeftPop<CustomerOrderRedisModel>();
            if (listLeftPop != null)
            {
                RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, ToFolderName), toState).SetHashKeyValue(listLeftPop.Key, listLeftPop);
                return listLeftPop;
            }
            return new CustomerOrderRedisModel();
        }
        catch (Exception)
        {
            return new CustomerOrderRedisModel();
        }

    }
    public CustomerOrderRedisModel GetDataChangeStateData(string redisCategory, string folderName, string toFolder, int fromState, int toState)
    {
        try
        {

            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var listLeftPop = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), fromState)
       .ListRightPopLeftPushWithKey(string.Format("{0}:{1}", redisCategory, toFolder), toState);

            if (!string.IsNullOrEmpty(listLeftPop.Error))
            {
                //_logger.LogInformation($"<FOMJobs>GetAndSetredisError:{listLeftPop.Error}</FOMJobs>");
                return null;

            }
            return listLeftPop.CustomerOrderRedisModel;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"<FOMJobs>GetAndSetredisHata:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }
        return null;

    }
    public async Task<CustomerOrderRedisModel> GetDataChangeStateDataAsync(string redisCategory, string folderName, string toFolder, int fromState, int toState)
    {
        try
        {

            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var listLeftPop = await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folderName), fromState)
       .ListRightPopLeftPushWithKeyAsync(string.Format("{0}:{1}", redisCategory, toFolder), toState);

            if (!string.IsNullOrEmpty(listLeftPop.Error))
            {
                //_logger.LogInformation($"<FOMJobs>GetAndSetredisError:{listLeftPop.Error}</FOMJobs>");
                return null;

            }
            return listLeftPop.CustomerOrderRedisModel;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"<FOMJobs>GetAndSetredisHata:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }
        return null;

    }
    public bool SetAndDeleteRedisDataWithKey<T>(string redisCategory, string deleteFolder, int deleteState, string deleteKey, string toFolder, int state, T model)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, toFolder), state)
                       .SetHashKeyValue(deleteKey, model);
            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, deleteFolder), deleteState)
                      .DeleteHashKey(deleteKey);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
    public bool SetNotProcessingDataWithKey(string redisCategory, string fromFolder, string paternKey, string toFolder, int toState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            var getKeyValues = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, fromFolder))
                                    .SetNotProcessingDataWithKey(string.Format("MediaMarktIT:FOM:{0}:{1}", fromFolder, paternKey), string.Format("MediaMarktIT:FOM:{0}:{1}", toFolder, toState));
            return getKeyValues;
        }
        catch (Exception)
        {

            return false;
        }
    }
    public bool ClearStateData(string redisCategory, string clearFolder, int clearState)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, clearFolder), clearState)
                       .ListLeftPop();

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");

        }

        return true;
    }
    public bool IsRedisDataWithKey(string redisCategory, string folder, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }

            var isRedisDataWithPattern = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folder), state)
                        .IsRedisDataWithPattern("MediaMarktIT:FOM:" + folder);

            return isRedisDataWithPattern;

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");
        }

        return false;
    }
    public bool IsRedisData(string redisCategory, string folder, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            var isRedisData = RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folder), state)
                        .IsRedisData();

            return isRedisData;

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");
        }

        return false;
    }
    public async Task<bool> IsRedisDataAsync(string redisCategory, string folder, int state)
    {
        try
        {
            if (!this.IsRedisConnect())
            {
                RedisConnect();
            }


            var isRedisData = await RedisHelper.GetRedisDB(string.Format("{0}:{1}", redisCategory, folder), state)
                        .IsRedisDataAsync();

            return isRedisData;

        }
        catch (Exception ex)
        {
            _logger.LogError($"<FOMJobs>SetErrorData:{ex.Message}-{ex.InnerException}</FOMJobs>");
        }

        return false;
    }
}
