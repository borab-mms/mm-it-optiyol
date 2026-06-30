using MM.IT.Common.Models.EKOLStock;
using MM.IT.Common.Models.MEX;
using MM.IT.Common.Models.Sterling;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.RedisAdaptor.Interfaces;

public interface IRedisDistributedAdapter
{
    void RedisConnect();
    bool IsRedisConnect();
    PaymentResponse GetRedisRawData(string redisCategory, string rawDataKey);
    PaymentResponse GetRedisData(string redisCategory, string folderName, int state);
    T GetRedisData<T>(string redisCategory, string folderName, int state);
    List<PaymentResponse> GetRedisAllData(string redisCategory, string folderName, int state);
    bool UpdateRedisState(string redisCategory, string folderName, int state, PaymentResponse paymentResponse);
    bool UpdateRedisState<T>(string redisCategory, string folderName, int state, T model);
    bool UpdateRedisState<T>(string redisCategory, string FromFolderName, string ToFolderName, string key, int toState, int fromState = 0);
    bool UpdateRedisStateWithTimeAndKey<T>(string redisCategory, string FromFolderName, string ToFolderName, string key, int fromState, int toState, int hour = 0, int day = 0); bool UpdateRedisStateWithTime<T>(string redisCategory, string folderName, int state, T model, int hour = 0, int day = 0);
    bool UpdateRedisStateWithTime(string redisCategory, string folderName, int state, PaymentResponse paymentResponse, int hour = 0, int day = 0);
    bool SetRedisData(string applicationCode, string redisCategory, string rawDataKey, object model, int state);
    bool SetRedisData<T>(string applicationCode, string redisCategory, string toFolder, T model, int state);
    Task<bool> SetRedisDataAsync<T>(string applicationCode, string redisCategory, string toFolder, T model, int state);
    bool DeleteRedisData(string redisCategory, string folderName, int state);
    bool DeleteRedisDataWithHashKey(string redisCategory, string folderName, string key, int state);
    Task<bool> DeleteRedisDataWithHashKeyAsync(string redisCategory, string folderName, string key, int state);
    bool SetRedisDataWithKey<T>(string redisCategory, string rawDataKey, T model, string key, int state);
    Task<bool> SetRedisDataWithKeyAsync<T>(string redisCategory, string rawDataKey, T model, string key, int state);
    List<T> GetRedisAllDataWithKey<T>(string redisCategory, string folderName, int state);
    bool AddOrUpdateRedisState<T>(string redisCategory, string folderName, T model, string key, int state);
    bool UpdateRedisStateWithStringKey<T>(string redisCategory, string folderName, int state, T model, DateTime expireDate);
    bool ChangeRedisState<T>(string redisCategory, string FromFolderName, string ToFolderName, int fromState, int toState);
    T ListRightPopLeftPush<T>(string redisCategory, string FromFolderName, int fromState, int nextState);
    bool SetRedisKey<T>(string redisCategory, string ToFolderName, T model, string key, int toState);
    List<CustomerOrderRedisModel> GetRedisKeyByCreatedDate(string redisCategory, string folderName, DateTime dateTime, int state);
    bool SetAndDeleteRedisData<T>(string redisCategory, string deleteFolder, int deleteState, string deleteKey, string toFolder, int state, T model);
    CustomerOrderRedisModel GetAndSetWaitingData(string redisCategory, string folderName, int fromState, string ToFolderName, int toState);
    bool SetAndDeleteRedisDataWithKey<T>(string redisCategory, string deleteFolder, int deleteState, string deleteKey, string toFolder, int state, T model);
    bool ChangeRedisStateData<T>(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, string actionName = null, string orderNo = null);
    Task<bool> ChangeRedisStateDataAsync<T>(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, string actionName = null, string orderNo = null);
    CustomerOrderRedisModel GetDataChangeStateData(string redisCategory, string folderName, string toFolder, int fromState, int toState);
    Task<CustomerOrderRedisModel> GetDataChangeStateDataAsync(string redisCategory, string folderName, string toFolder, int fromState, int toState);
    bool ChangeModelData(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, CustomerOrderRedisModel model, string actionName = null, string orderNo = null);
    Task<bool> ChangeModelDataAsync(string redisCategory, string fromFolder, int fromState, string toFolder, int toState, CustomerOrderRedisModel model, string actionName = null, string orderNo = null);
    bool SetErrorData(string redisCategory, string deleteFolder, int deleteState);
    Task<bool> SetErrorDataAsync(string redisCategory, string deleteFolder, int deleteState);
    bool SetNotProcessingDataWithKey(string redisCategory, string fromFolder, string paternKey, string toFolder, int toState);
    bool ClearStateData(string redisCategory, string clearFolder, int clearState);
    bool IsRedisData(string redisCategory, string folder, int state);
    Task<bool> IsRedisDataAsync(string redisCategory, string folder, int state);
    bool IsRedisDataWithKey(string redisCategory, string folder, int state);
}
