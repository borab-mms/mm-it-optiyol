using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// TotalVats Tablosu 
/// </summary>
[Table("TotalVats", Schema = "VCR")]

public class VCRTotalVatEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public decimal? InvoiceNetAmount { get; set; }
    public decimal? InvoiceVatAmount { get; set; }
    public decimal? InvoiceVatRate { get; set; }
}
