using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// OrderPayments Tablosu 
/// </summary>
[Table("OrderPayments", Schema = "MEX")]
public class MEXOrderPaymentEntity : BaseEntity<int>
{
    public string? OrderId { get; set; }
    public bool? IsSuccess { get; set; }
    public bool? IsCancelled { get; set; }
    public decimal? AmountRequested { get; set; }
    public decimal? AmountProcessed { get; set; }
    public string? InstrumentType { get; set; }
    public string? InstrumentDetailCardProcessType { get; set; }
    public DateTime? TransactionDate { get; set; }
}
