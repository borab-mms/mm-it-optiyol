using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderItemCharges Tablosu 
/// </summary>
[Table("HistoryOrderItemCharges", Schema = "Sterling")]
public class FOMHistoryOrderItemChargeEntity : BaseEntity<int>
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
    /// ItemChargeType Bilgisi
    /// </summary>
    public string? ItemChargeType { get; set; }

    /// <summary>
    /// ItemChargeDescription Bilgisi
    /// </summary>
    /// 
    public string? ItemChargeDescription { get; set; }

    /// <summary>
    /// ItemChargeValue Bilgisi
    /// </summary>
    public string? ItemChargeValue { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}