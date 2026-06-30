using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;


/// <summary>
/// OrderShipmentLines Tablosu 
/// </summary>
[Table("OrderShipmentLines", Schema = "Sterling")]
public class FOMOrderShipmentLineEntity : BaseEntity<int>
{
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }
    /// <summary>
    /// ShipmentLineNo Bilgisi
    /// </summary>
    public string? ShipmentLineNo { get; set; }

    /// <summary>
    /// SalesDocNumber Bilgisi
    /// </summary>
    public string? SalesDocNumber { get; set; }

    /// <summary>
    /// FulfillmentOrderID Bilgisi
    /// </summary>
    public string? FulfillmentOrderID { get; set; }

    /// <summary>
    /// LineItemRef Bilgisi
    /// </summary>
    /// 
    public string? LineItemRef { get; set; }

    /// <summary>
    /// Quantity Bilgisi
    /// </summary>
    public string? Quantity { get; set; }

    /// <summary>
    /// SerialNumbers Bilgisi
    /// </summary>
    public string? SerialNumbers { get; set; }

    /// <summary>
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}