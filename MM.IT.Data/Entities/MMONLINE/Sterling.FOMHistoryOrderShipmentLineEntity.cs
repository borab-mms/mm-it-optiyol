using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;


/// <summary>
/// HistoryOrderShipmentLines Tablosu 
/// </summary>
[Table("HistoryOrderShipmentLines", Schema = "Sterling")]
public class FOMHistoryOrderShipmentLineEntity : BaseEntity<int>
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
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}