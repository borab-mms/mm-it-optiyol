using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderDiscount Tablosu 
/// </summary>
[Table("HistoryOrderDiscount", Schema = "Sterling")]
public class FOMHistoryOrderDiscountEntity : BaseEntity<int>
{
    /// <summary>
    /// OrderDiscountType Bilgisi
    /// </summary>
    public string? OrderDiscountType { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// OrderDiscountCode Bilgisi
    /// </summary>
    /// 
    public string? OrderDiscountCode { get; set; }

    /// <summary>
    /// OrderDiscountDescription Bilgisi
    /// </summary>
    public string? OrderDiscountDescription { get; set; }

    /// <summary>
    /// OrderDiscountValue Bilgisi
    /// </summary>
    public string? OrderDiscountValue { get; set; }

    /// <summary>
    /// OrderDiscountReasonCode Bilgisi
    /// </summary>
    public string? OrderDiscountReasonCode { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}