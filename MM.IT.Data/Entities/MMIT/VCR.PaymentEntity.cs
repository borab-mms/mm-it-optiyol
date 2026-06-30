using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// Payments Tablosu 
/// </summary>
[Table("Payments", Schema = "VCR")]
public class VCRPaymentEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public string? PaymentChannelType { get; set; }
    public decimal? PaymentChannelAmount { get; set; }
    public string? PaymentChannelDetail { get; set; }
}

