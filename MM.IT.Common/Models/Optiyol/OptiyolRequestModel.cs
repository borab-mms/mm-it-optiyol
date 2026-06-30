using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MM.Optiyol.Api.Utilities.Extensions;

namespace MM.Optiyol.Api.Models.Optiyol
{
    public class OptiyolRequestModel
    {
        [JsonPropertyName("hook")]
        //[Required]
        public OptiyolRequestHookModel Hook { get; set; }

        [JsonPropertyName("data")]
        //[Required]
        public object Data { get; set; }

    }

    public class OptiyolRequestHookModel
    {
        [JsonPropertyName("id")]
        //[Required]
        public int Id { get; set; }

        [JsonPropertyName("event")]
        //[Required]
        public string Event { get; set; }

        [JsonPropertyName("target")]
        //[Required]
        public string Target { get; set; }
    }

    public class OrderArrivedModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("is_arrived")]
        public bool IsArrived { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("arrived_time")]
        public DateTime ArrivedTime { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("arrival_time")]
        public DateTime ArrivalTime { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("otp")]
        public string Otp { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("recipients")]
        public string[] Recipients { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }
    }

    public class OrderCompletedModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("completed_time")]
        public DateTime CompletedTime { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("planned_complete_time")]
        public DateTime PlannedCompleteTime { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("is_delivered_to_owner")]
        [JsonConverter(typeof(BoolOrEmptyStringToStringConverter))]
        public string IsDeliveredToOwner { get; set; }

        //[Required]
        [JsonPropertyName("contact_person_relation")]
        public string ContactPersonRelation { get; set; }

        //[Required]
        [JsonPropertyName("contact_person")]
        public string ContactPerson { get; set; }

        //[Required]
        [JsonPropertyName("contact_person_phone")]
        public string ContactPersonPhone { get; set; }

        //[Required]
        [JsonPropertyName("customer_note")]
        public string CustomerNote { get; set; }

        //[Required]
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        //[Required]
        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("route_start_time")]
        public DateTime RouteStartTime { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }
    }

    public class OrderCompletedWithItemModel
    {
        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public double LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public double LocationLon { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }

        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("claim_id")]
        public string ClaimId { get; set; }

        [JsonPropertyName("channel_extra_data")]
        public object ChannelExtraData { get; set; }

        //[Required]
        [JsonPropertyName("order_pk")]
        public int OrderPk { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        //[Required]
        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }

        [JsonPropertyName("completed_time")]
        public string CompletedTime { get; set; }

        [JsonPropertyName("planned_complete_time")]
        public string PlannedCompleteTime { get; set; }

        //[Required]
        [JsonPropertyName("is_delivered_to_owner")]
        public bool IsDeliveredToOwner { get; set; }

        [JsonPropertyName("contact_person_relation")]
        public string ContactPersonRelation { get; set; }

        [JsonPropertyName("contact_person")]
        public string ContactPerson { get; set; }

        [JsonPropertyName("contact_person_phone")]
        public string ContactPersonPhone { get; set; }

        [JsonPropertyName("customer_note")]
        public string CustomerNote { get; set; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        //[Required]
        [JsonPropertyName("items")]
        public List<OrderCompletedWithItemsModel> Items { get; set; }
    }

    public class OrderCompletedWithItemsModel
    {
        [JsonPropertyName("order_item_id")]
        public string OrderItemId { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }

        [JsonPropertyName("sku_id")]
        public string SkuId { get; set; }

        [JsonPropertyName("sku_name")]
        public string SkuName { get; set; }

        [JsonPropertyName("uom_quantity")]
        public string UomQuantity { get; set; }

        //[Required]
        [JsonPropertyName("planned_sku_quantity")]
        public int PlannedSkuQuantity { get; set; }

        //[Required]
        [JsonPropertyName("actual_sku_quantity")]
        public int ActualSkuQuantity { get; set; }

        //[Required]
        [JsonPropertyName("actual_sku_lot_numbers")]
        public List<ActualSkuLotNumberModel> ActualSkuLotNumbers { get; set; }

        //[Required]
        [JsonPropertyName("actual_sku_percentage_quantity")]
        public double ActualSkuPercentageQuantity { get; set; }

        //[Required]
        [JsonPropertyName("planned_price")]
        public double PlannedPrice { get; set; }

        //[Required]
        [JsonPropertyName("actual_price")]
        public double ActualPrice { get; set; }

        //[Required]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("driver_note")]
        public string DriverNote { get; set; }

        [JsonPropertyName("cancel_reason")]
        public string CancelReason { get; set; }
    }

    public class ActualSkuLotNumberModel
    {
        //[Required]
        [JsonPropertyName("lotNumber")]
        public string LotNumber { get; set; }

        //[Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class RoutePlannedModel
    {
        //[Required]
        [JsonPropertyName("route_tracker_code")]
        public string RouteTrackerCode { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        //[Required]
        [JsonPropertyName("driver_firstname")]
        public string DriverFirstName { get; set; }

        //[Required]
        [JsonPropertyName("driver_lastname")]
        public string DriverLastName { get; set; }

        //[Required]
        [JsonPropertyName("driver_email")]
        public string DriverEmail { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        //[Required]
        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }
    }

    public class RouteLoadListCompletedModel
    {
        //[Required]
        [JsonPropertyName("route_tracker_code")]
        public string RouteTrackerCode { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("load_completion_time")]
        public DateTime LoadCompletionTime { get; set; }

        //[Required]
        [JsonPropertyName("items")]
        public List<RouteLoadListCompletedItemModel> Items { get; set; }
    }

    public class RouteLoadListCompletedItemModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        //[Required]
        [JsonPropertyName("order_item_id")]
        public string OrderItemId { get; set; }

        //[Required]
        [JsonPropertyName("sku_id")]
        public string SkuId { get; set; }

        //[Required]
        [JsonPropertyName("sku_name")]
        public string SkuName { get; set; }

        //[Required]
        [JsonPropertyName("sku_load_quantity")]
        public int SkuLoadQuantity { get; set; }

        //[Required]
        [JsonPropertyName("lot_number")]
        public string LotNumber { get; set; }

        //[Required]
        [JsonPropertyName("uom_quantity")]
        public string UomQuantity { get; set; }
    }

    public class RouterStartedModel
    {
        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        //[Required]
        [JsonPropertyName("route_tracker_code")]
        public string RouteTrackerCode { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        //[Required]
        [JsonPropertyName("number_of_stops")]
        public int NumberOfStops { get; set; }

        //[Required]
        [JsonPropertyName("number_of_orders")]
        public int NumberOfOrders { get; set; }

        //[Required]
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        //[Required]
        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        //[Required]
        [JsonPropertyName("orders")]
        public List<RouterStartedOrderModel> Orders { get; set; }
    }

    public class RouterStartedOrderModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("sequence")]
        public int Sequence { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("delivery_time_lower")]
        public string DeliveryTimeLower { get; set; }

        //[Required]
        [JsonPropertyName("delivery_time_upper")]
        public string DeliveryTimeUpper { get; set; }

        //[Required]
        [JsonPropertyName("is_loaded")]
        public bool IsLoaded { get; set; }

        //[Required]
        [JsonPropertyName("track_url")]
        public string TrackUrl { get; set; }
    }

    public class OrderCanceledModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("canceled_time")]
        public DateTime CanceledTime { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        //[Required]
        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        [JsonPropertyName("cancel_reason")]
        public string CancelReason { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }
    }

    public class OrderCanceledWithItemsModel
    {
        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }

        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("claim_id")]
        public string ClaimId { get; set; }

        [JsonPropertyName("channel_extra_data")]
        public object ChannelExtraData { get; set; }

        //[Required]
        [JsonPropertyName("order_pk")]
        public int OrderPk { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        //[Required]
        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }

        [JsonPropertyName("canceled_time")]
        public string CanceledTime { get; set; }

        [JsonPropertyName("cancel_reason")]
        public string CancelReason { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        //[Required]
        [JsonPropertyName("items")]
        public List<OrderCanceledWithItemListModel> Items { get; set; }
    }

    public class OrderCanceledWithItemListModel
    {
        [JsonPropertyName("order_item_id")]
        public string OrderItemId { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; }

        [JsonPropertyName("sku_id")]
        public string SkuId { get; set; }

        [JsonPropertyName("sku_name")]
        public string SkuName { get; set; }

        [JsonPropertyName("uom_quantity")]
        public string UomQuantity { get; set; }

        //[Required]
        [JsonPropertyName("planned_sku_quantity")]
        public int PlannedSkuQuantity { get; set; }

        //[Required]
        [JsonPropertyName("actual_sku_quantity")]
        public int ActualSkuQuantity { get; set; }

        //[Required]
        [JsonPropertyName("actual_sku_percentage_quantity")]
        public double ActualSkuPercentageQuantity { get; set; }

        //[Required]
        [JsonPropertyName("planned_price")]
        public double PlannedPrice { get; set; }

        //[Required]
        [JsonPropertyName("actual_price")]
        public double ActualPrice { get; set; }

        //[Required]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("driver_note")]
        public string DriverNote { get; set; }

        [JsonPropertyName("cancel_reason")]
        public string CancelReason { get; set; }
    }

    public class OrderUndoCanceledModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }

        //[Required]
        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("undo_canceled_time")]
        public DateTime UndoCanceledTime { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }
    }

    public class OrderReturnedModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        //[Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }

        //[Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }

        //[Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        //[Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }

        //[Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }

        //[Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }

        //[Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }

        //[Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }

        //[Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonPropertyName("returned_lat")]
        public string ReturnedLat { get; set; }

        //[Required]
        [JsonPropertyName("returned_lon")]
        public string ReturnedLon { get; set; }

        //[Required]
        [JsonPropertyName("is_returned")]
        public bool IsReturned { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("returned_time")]
        public DateTime ReturnedTime { get; set; }

        //[Required]
        [JsonPropertyName("returned_location")]
        public string ReturnedLocation { get; set; }

        //[Required]
        [JsonPropertyName("contact_person_relation")]
        public string ContactPersonRelation { get; set; }

        //[Required]
        [JsonPropertyName("contact_person")]
        public string ContactPerson { get; set; }

        [JsonPropertyName("contact_person_phone")]
        public string ContactPersonPhone { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        //[Required]
        [JsonPropertyName("images")]
        public List<string> Images { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }        

        //[Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }
    }

    public class RouteFinishedModel
    {
        //[Required]
        [JsonPropertyName("route_tracker_code")]
        public string RouteTrackerCode { get; set; }

        //[Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }

        //[Required]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("finish_time")]
        public DateTime FinishTime { get; set; }

        //[Required]
        [JsonPropertyName("number_of_stops")]
        public int NumberOfStops { get; set; }

        //[Required]
        [JsonPropertyName("number_of_orders")]
        public int NumberOfOrders { get; set; }

        //[Required]
        [JsonPropertyName("number_of_completed_orders")]
        public int NumberOfCompletedOrders { get; set; }

        //[Required]
        [JsonPropertyName("number_of_not_completed_orders")]
        public int NumberOfNotCompletedOrders { get; set; }

        //[Required]
        [JsonPropertyName("number_of_canceled_orders")]
        public int NumberOfCanceledOrders { get; set; }

        //[Required]
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        //[Required]
        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        //[Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        //[Required]
        [JsonPropertyName("orders")]
        public List<RouteFinishedOrderModel> Orders { get; set; }
    }

    public class RouteFinishedOrderModel
    {
        //[Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    }
    public class OrderPickupVirtualCompletedModel
    {
        // [Required]
        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }
        // [Required]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        // [Required]
        [JsonPropertyName("visit_sequence")]
        public int VisitSequence { get; set; }
        // [Required]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }
        // [Required]
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }
        // [Required]
        [JsonPropertyName("is_dropoff")]
        public bool IsDropoff { get; set; }
        // [Required]
        [JsonPropertyName("is_pickup")]
        public bool IsPickup { get; set; }
        // [Required]
        [JsonPropertyName("location_id")]
        public string LocationId { get; set; }
        // [Required]
        [JsonPropertyName("location_address")]
        public string LocationAddress { get; set; }
        // [Required]
        [JsonPropertyName("location_lat")]
        public string LocationLat { get; set; }
        // [Required]
        [JsonPropertyName("location_lon")]
        public string LocationLon { get; set; }
        // [Required]
        [JsonPropertyName("is_location_verified")]
        public bool IsLocationVerified { get; set; }
        // [Required]
        [JsonPropertyName("vehicle_code")]
        public string VehicleCode { get; set; }
        // [Required]
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        // [Required]
        [JsonPropertyName("claim_id")]
        public string ClaimId { get; set; }
        //// [Required]
        //[JsonPropertyName("channel_extra_data")]
        //public List<string> ChannelExtraData { get; set; }
        // [Required]
        [JsonPropertyName("order_pk")]
        public long OrderPK { get; set; }
        // [Required]
        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }
        //[Required]
        [JsonPropertyName("images")]
        public List<string> Images { get; set; }
        // [Required]
        [JsonPropertyName("arrived_lat")]
        public string ArrivedLat { get; set; }
        // [Required]
        [JsonPropertyName("arrived_lon")]
        public string ArrivedLon { get; set; }
        // [Required]
        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }
        // [Required]
        [JsonPropertyName("route_start_time")]
        public string RouteStartTime { get; set; }
        // [Required]
        [JsonPropertyName("completed_time")]
        public string CompletedTime { get; set; }
        // [Required]
        [JsonPropertyName("planned_complete_time")]
        public string PlannedCompleteTime { get; set; }
        // [Required]
        [JsonPropertyName("is_delivered_to_owner")]
        public bool IsDeliveredToOwner { get; set; }
        // [Required]
        [JsonPropertyName("contact_person_relation")]
        public string ContactPersonRelation { get; set; }
        // [Required]
        [JsonPropertyName("contact_person")]
        public string ContactPerson { get; set; }
        // [Required]
        [JsonPropertyName("contact_person_phone")]
        public string ContactPersonPhone { get; set; }
        // [Required]
        [JsonPropertyName("customer_note")]
        public string CustomerNote { get; set; }
        // [Required]
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }
        // [Required]
        [JsonPropertyName("note")]
        public string Note { get; set; }
        // [Required]
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    }

}
