using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderShipments Tablosu 
/// </summary>
[Table("HistoryOrderShipments", Schema = "Sterling")]
public class FOMHistoryOrderShipmentEntity : BaseEntity<int>
{
    /// <summary>
    /// ShipmentNumber Bilgisi
    /// </summary>
    public string? ShipmentNumber { get; set; }
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// Status Bilgisi
    /// </summary>
    /// 
    public string? Status { get; set; }

    /// <summary>
    /// ShipNode Bilgisi
    /// </summary>
    public string? ShipNode { get; set; }

    /// <summary>
    /// SalesDocNumber Bilgisi
    /// </summary>
    public string? SalesDocNumber { get; set; }

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
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

}