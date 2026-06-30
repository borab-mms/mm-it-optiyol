using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// Logs Tablosu 
/// </summary>

[Table("Logs", Schema = "VCR")]
public class VCRLogEntity : BaseEntity<int>
{
    public string? ServiceName { get; set; }
    public string? CustomerOrderNumber { get; set; }
    public string? Request { get; set; }
    public string? Response { get; set; }
    public string? InvoiceId { get; set; }
    public bool? IsSuccess { get; set; }
}

