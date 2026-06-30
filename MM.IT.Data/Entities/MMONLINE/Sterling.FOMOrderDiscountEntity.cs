using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// OrderDiscount Tablosu 
/// </summary>
[Table("OrderDiscount", Schema = "Sterling")]
public class FOMOrderDiscountEntity : BaseEntity<int>
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
    /// Güncelleme tarihi bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}