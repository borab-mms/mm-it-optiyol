using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// TotalDiscounts Tablosu 
/// </summary>
[Table("TotalDiscounts", Schema = "VCR")]
public class VCRTotalDiscountEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public decimal? TotalDiscountAmount { get; set; }
    public string? TotalDiscountAmountInWords { get; set; }
    public decimal? TotalDiscountNetAmount { get; set; }
    public decimal? TotalDiscountVatAmount { get; set; }
}

