using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderShipments Tablosu 
/// </summary>
[Table("OrderShipments", Schema = "Sterling")]
public class FOMOrderShipmentEntity : BaseEntity<int>
{
    /// <summary>
    /// ShipmentNumber Bilgisi
    /// </summary>
    public string? ShipmentNumber { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string? CustomerOrderNumber { get; set; }

    /// <summary>
    /// Status Bilgisi
    /// </summary>
    /// 
    public string? Status { get; set; }

    /// <summary>
    /// ShipNode Bilgisi
    /// </summary>
    public int? ShipNode { get; set; }

    /// <summary>
    /// SalesDocNumber Bilgisi
    /// </summary>
    public int? SalesDocNumber { get; set; }

    /// <summary>
    /// FulfillmentOrderID Bilgisi
    /// </summary>
    public string? FulfillmentOrderID { get; set; }

    /// <summary>
    /// CarrierName Bilgisi
    /// </summary>
    public string? CarrierName { get; set; }

    /// <summary>
    /// PickupLocation Bilgisi
    /// </summary>
    public string? PickupLocation { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

}