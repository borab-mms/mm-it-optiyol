using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;


/// <summary>
/// HistoryOrderItemFulFillmentDelivery Tablosu 
/// </summary>
[Table("HistoryOrderItemFulFillmentDelivery", Schema = "Sterling")]
public class FOMHistoryOrderItemFulFillmentDeliveryEntity : BaseEntity<int>
{
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }
    /// <summary>
    /// ProductId Bilgisi
    /// </summary>
    /// 
    public string ProductId { get; set; }

    /// <summary>
    /// LineItemId Bilgisi
    /// </summary>
    /// 
    public string LineItemId { get; set; }

    /// <summary>
    /// LineItemReference Bilgisi
    /// </summary>
    /// 
    public string LineItemReference { get; set; }

    /// <summary>
    /// LineItemStatusDescription Bilgisi
    /// </summary>
    /// 
    public string LineItemStatusDescription { get; set; }
    /// <summary>
    /// ShipmentNumber Bilgisi
    /// </summary>
    public string? ShipmentNumber { get; set; }

    /// <summary>
    /// DeliveryDatePromise Bilgisi
    /// </summary>
    /// 
    public string? DeliveryDatePromise { get; set; }

    /// <summary>
    /// DeliveryDateActual Bilgisi
    /// </summary>
    public string? DeliveryDateActual { get; set; }

    /// <summary>
    /// WddFromDeliveryTime Bilgisi
    /// </summary>
    public string? WddFromDeliveryTime { get; set; }

    /// <summary>
    /// WddToDeliveryTime Bilgisi
    /// </summary>
    public string? WddToDeliveryTime { get; set; }

    /// <summary>
    /// CarrierName Bilgisi
    /// </summary>
    public string? CarrierName { get; set; }

    /// <summary>
    /// QuantityShipped Bilgisi
    /// </summary>
    public string? QuantityShipped { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

}