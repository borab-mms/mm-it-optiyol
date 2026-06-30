using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderItemFulFillments Tablosu 
/// </summary>
[Table("HistoryOrderItemFulFillments", Schema = "Sterling")]
public class FOMHistoryOrderItemFulFillmentEntity : BaseEntity<int>
{
    /// <summary>
    /// FulfillmentOrderId Bilgisi
    /// </summary>
    public string? FulfillmentOrderId { get; set; }
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
    /// FulfillmentMethod Bilgisi
    /// </summary>
    /// 
    public string? FulfillmentMethod { get; set; }

    /// <summary>
    /// FulfillmentOutletProcureNodeId Bilgisi
    /// </summary>
    public string? FulfillmentOutletProcureNodeId { get; set; }

    /// <summary>
    /// FulfillmentOutletShipNodeId Bilgisi
    /// </summary>
    public string? FulfillmentOutletShipNodeId { get; set; }

    /// <summary>
    /// FulfillmentQuantity Bilgisi
    /// </summary>
    public string? FulfillmentQuantity { get; set; }

    /// <summary>
    /// PickupDate Bilgisi
    /// </summary>
    public string? PickupDate { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

}