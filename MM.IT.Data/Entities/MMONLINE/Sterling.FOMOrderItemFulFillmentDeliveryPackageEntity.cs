
using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;


/// <summary>
/// OrderItemFulFillmentDeliveryPackages Tablosu 
/// </summary>
[Table("OrderItemFulFillmentDeliveryPackages", Schema = "Sterling")]
public class FOMOrderItemFulFillmentDeliveryPackageEntity : BaseEntity<int>
{
    /// <summary>
    /// PackageNumber Bilgisi
    /// </summary>
    public string? PackageNumber { get; set; }
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }
    /// <summary>
    /// FulfillmentOrderId Bilgisi
    /// </summary>
    /// 
    public string FulfillmentOrderId { get; set; }
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
    /// TrackingURL Bilgisi
    /// </summary>
    /// 
    public string? TrackingURL { get; set; }

    /// <summary>
    /// TrackingId Bilgisi
    /// </summary>
    public string? TrackingId { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}