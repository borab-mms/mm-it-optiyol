using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace MM.Optiyol.Api.Models.Optiyol;
public class OptiyolBarcodeCreateRequestModel
{
    [JsonProperty("barcodesPlannedDate")]
    [Required]
    public DateTime BarcodesPlannedDate { get; set; }

    [JsonProperty("barcodesPlannedTime")]
    public TimeSpan? BarcodesPlannedTime { get; set; }

    [JsonProperty("barcodes")]
    public List<BarcodeModel> Barcodes { get; set; }
}

public class BarcodeModel
{

    [Required]
    [JsonProperty("serviceType")]
    public string ServiceType { get; set; }

    [Required]
    [JsonProperty("fromLocationId")]
    public string FromLocationId { get; set; }

    [Required]
    [JsonProperty("toLocationId")]
    public string ToLocationId { get; set; }

    [Required]
    [JsonProperty("toLocationName")]
    public string ToLocationName { get; set; }

    [Required]
    [JsonProperty("toLocationAddress")]
    public string ToLocationAddress { get; set; }

    [Required]
    [JsonProperty("toLocationCounty")]
    public string ToLocationCounty { get; set; }

    [Required]
    [JsonProperty("toLocationCity")]
    public string ToLocationCity { get; set; }

    [Required]
    [JsonProperty("toLocationLatitude")]
    public string ToLocationLatitude { get; set; }

    [Required]
    [JsonProperty("toLocationLongitude")]
    public string ToLocationLongitude { get; set; }

    [JsonProperty("toLocationPhone")]
    public string ToLocationPhone { get; set; }

    [Required]
    [JsonProperty("volume")]
    public decimal Volume { get; set; } = 0;

    [JsonProperty("weight")]
    public decimal Weight { get; set; } = 0;

    [JsonProperty("note")]
    public string Note { get; set; }

    [JsonProperty("requiresPaymentAtDoor")]
    public bool? RequiresPaymentAtDoor { get; set; }

    [JsonProperty("paymentMethod")]
    public string PaymentMethod { get; set; }

    [JsonProperty("paymentAmount")]
    public decimal PaymentAmount { get; set; }

    [JsonProperty("earliestPickupTime")]
    public TimeSpan? EarliestPickupTime { get; set; }

    [JsonProperty("latestPickupTime")]
    public TimeSpan? LatestPickupTime { get; set; }

    [JsonProperty("earliestDeliveryTime")]
    public TimeSpan? EarliestDeliveryTime { get; set; }

    [JsonProperty("latestDeliveryTime")]
    public TimeSpan? LatestDeliveryTime { get; set; }

    [JsonProperty("requiredVehicleProperties")]
    public List<string> RequiredVehicleProperties { get; set; }

    [JsonProperty("barcodeProperties")]
    public List<string> BarcodeProperties { get; set; }
}

public class OptiyolBarcodeCreateResponseModel
{
    [JsonProperty("result")]
    public bool Result { get; set; }
    [JsonProperty("created_orders_count")]
    public int CreatedOrdersCount { get; set; }
    [JsonProperty("info")]
    public string Info { get; set; }
}