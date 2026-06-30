using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// Redis Config Nesnesi -> appsettings.config: Redis eşleniği
/// Cache Ayarlarını Saklar.
/// </summary>
public class RedisConfigModel
{
    public string Name { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
    public string ApplicationCode { get; set; }
    public string SiteCode { get; set; }
    public MEXRedisSetting MEXRedisSettings { get; set; }
    public EKOLStockRedisSetting EKOLStockRedisSettings { get; set; }
    public MarketPlaceRedisSetting MarketPlaceRedisSettings { get; set; }
    public SMSRedisSetting SMSRedisSettings { get; set; }
    public FOMRedisSetting FOMRedisSettings { get; set; }
  
}
public class MEXRedisSetting
{
    public string RedisCategory { get; set; }
    public string RedisSubCategory { get; set; }
    public string RawDataKey { get; set; }
    public string ProcessingDataKey { get; set; }
    public string SuccessfullDataKey { get; set; }
    public string UnSuccessfullDataKey { get; set; }
    public string Ld00Data { get; set; }
    public string NotLd00Data { get; set; }
    public string ParsedData { get; set; }
    public string WCSStatusEqualRData { get; set; }
    public string WCSStatusNotEqualRData { get; set; }
    public string WCSStatusNotEqualRSubData { get; set; }
    public string WSCData { get; set; }
    public string UnpaidData { get; set; }
    public string UnpaidSavedData { get; set; }
    public string WSCOrderExportedSuccessfullyData { get; set; }
}
public class EKOLStockRedisSetting
{
    public string EKOLKey { get; set; }
    public string RedisCategory { get; set; }
    public string RedisSubCategory { get; set; }
    public string EKOLStockRawDataKey { get; set; }
    public string EKOLStockDataParsedIntoDB { get; set; }
    public string SuccessfullDataKey { get; set; }
    public string UnSuccessfullDataKey { get; set; }
    public string EKOLDesiRawDataKey { get; set; }
    public string EKOLDesiDataParsedIntoDB { get; set; }
}
public class MarketPlaceRedisSetting
{
    public OrderStatusSetting OrderStatusSettings { get; set; }
    public OrderSetting OrderSettings { get; set; }
    public OrderCancellationSetting OrderCancellationSettings { get; set; }
}
public class OrderStatusSetting
{
    public string MarketPlaceKey { get; set; }
    public string RedisCategory { get; set; }
    public string OrderStatusKey { get; set; }
    public string StatuTypeEqualOrderData { get; set; }
    public string StatuTypeEqualReturnData { get; set; }
    public string OrderStatusUpdateData { get; set; }
    public string OrderStatusUpdateCompletedData { get; set; }
    public string Order_CheckStatusData { get; set; }
    public string Order_DBUpdateData { get; set; }
    public string Order_OrderCreationWaitingData { get; set; }
    public string OrderCancel_CancellationProcessData { get; set; }
    public string OrderCancel_EkolUpdateData { get; set; }
    public string Order_StatusCheckData { get; set; }
    public string OrderCancel_FomCheckData { get; set; }
    public string Order_FomWaitingData { get; set; }
    public string Return_DBUpdateData { get; set; }
    public string OrderCancel_CheckStoreData { get; set; }
    public string OrderCancel_CheckWarehouseData { get; set; }
    public string OrderCancel_UpdateArvatoData { get; set; }
    public string OrderCancel_OfflineStoreData { get; set; }
    public string OrderCancel_WarehouseSearchData { get; set; }
    public string Order_UpdateCompletedData { get; set; }
    public string Return_UpdateCompletedData { get; set; }
    public string OrderCancel_FailNotificationData { get; set; }
    public string OrderCancel_UnsuccessfulListData { get; set; }
}
public class OrderSetting
{
    public string MarketPlaceKey { get; set; }
    public string RedisCategory { get; set; }
    public string Order_NewData { get; set; }
    public string Order_VatIssuesData { get; set; }
    public string T601_StockCheckData{ get; set; }
    public string T601_WarehouseCheckData { get; set; }
    public string T800_ExcludeCheckData{ get; set; }
    public string SendToFOM_HomeDeliveryData{ get; set; }
    public string T800_StockCheckData{ get; set; }
    public string SendToFOM_ShpFromStoreData{ get; set; }
    public string Order_ESBSuccessfulData{ get; set; }
    public string Order_ESBUnSuccessfulData { get; set; }
    public string AyenSoft_OrderNoNotficationData { get; set; }
    public string Ayensoft_OrderNoNotificationIssueData{ get; set; }
}
public class SMSRedisSetting
{
    public string RedisCategory { get; set; }
    public string SingleRawData { get; set; }
    public string SingleProcessingData { get; set; }
    public string MultiRawData { get; set; }
    public string MultiProcessingData { get; set; }
    public string OTPRawData { get; set; }
    public string OTPProcessingData { get; set; }
}
public class OrderCancellationSetting
{
    public string MarketPlaceKey { get; set; }
    public string RedisCategory { get; set; }
    public string OrderCancellation_NewData { get; set; }
    public string Cancel_ESBWaitingData { get; set; }
    public string Cancel_InArvatoData { get; set; }
    public string Cancel_InEkolData { get; set; }
    public string Cancel_InStoreData { get; set; }
    public string Cancel_SuccessfulData { get; set; }
    public string Cancel_NotCanceledNotfData { get; set; }
    public string Cancel_DbUpdateData { get; set; }
    public string Cancel_NotCanceledData { get; set; }
}
public class FOMRedisSetting
{
    public string RedisCategory { get; set; }
    public string FirstData { get; set; }
    public string FirstWaitingData { get; set; }
    public string RawDataCheckData { get; set; }
    public string RawData { get; set; }
    public string RawWaitingData { get; set; }
    public string RawProcessingData { get; set; }
    public string RawMovingData { get; set; }
    public string NewData { get; set; }
    public string NewDataWaitingData { get; set; }
    public string NewOrderData { get; set; }
    public string NewOrderWaitingData { get; set; }
    public string OrderUpdateData { get; set; }
    public string OrderUpdateWaitingData { get; set; }
    public string OrderDbInsertData { get; set; }
    public string OrderDbInsertWaitingData { get; set; }
    public string OrderInsertErrorData { get; set; }
    public string SMSSendData { get; set; }
    public string SMSSendWaitingData { get; set; }
    public string FlowCheckData { get; set; }
    public string FlowCheckWaitingData { get; set; }
    public string AGTData { get; set; }
    public string AGTWaitingData { get; set; }
    public string SFSLCCheckData { get; set; }
    public string SFSLCCheckWaitingData { get; set; }
    public string PUData { get; set; }
    public string PUWaitingData { get; set; }
    public string HDData { get; set; }
    public string HDWaitingData { get; set; }
    public string AGTErrorData { get; set; }
    public string AGTErrorWaitingData { get; set; }
    public string OrderDbParseData { get; set; }
    public string OrderDbParseWaitingData { get; set; }
    public string HistoryData { get; set; }
    public string HistoryWaitingData { get; set; }
    public string OrderDbParseErrorData { get; set; }
    public string OrderDbParseErrorWaitingData { get; set; }
    public string DBInsertCheckData { get; set; }
    public string DBInsertCheckWaitingData { get; set; }
    public string EndOfFlowData { get; set; }
    public string EndOfFlowWaitingData { get; set; }
    public string HistoryErrorData { get; set; }
    public string HistoryErrorWaitingData { get; set; }
    public string OrderDbUpdateData { get; set; }
    public string OrderDbUpdateWaitingData { get; set; }
    public string VCRData { get; set; }
    public string VCRWaitingData { get; set; }
    public string VCRErrorData { get; set; }
    public string VCRErrorWaitingData { get; set; }
    public string T800MPData { get; set; }
    public string T800MPWaitingData { get; set; }
    public string T800MPErrorData { get; set; }
    public string T800MPErrorWaitingData { get; set; }
    public string ShippedData { get; set; }
    public string ShippedWaitingData { get; set; }
    public string SendToArvatoData { get; set; }
    public string SendToArvatoWaitingData { get; set; }
    public string SendToArvatoErrorData { get; set; }
    public string SendToArvatoErrorWaitingData { get; set; }
    public string OrderDBInsertDataErrorData { get; set; }
    public string OrderDBInsertDataErrorWaitingData { get; set; }
    public string BackOrderData { get; set; }
    public string BackOrderWaitingData { get; set; }
    public string PimData { get; set; }
    public string PimWaitingData { get; set; }
    public string SendToFspData { get; set; }
    public string SendToFspWaitingData { get; set; }
    public string MasterDataStatusListData { get; set; }
    public string MasterDataStatusListWaitingData { get; set; }
}

