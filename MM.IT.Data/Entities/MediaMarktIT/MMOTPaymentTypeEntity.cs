using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;


/// <summary>
/// OT_PaymentTypes Tablosu 
/// </summary>
[Table("OT_PaymentTypes", Schema = "dbo")]

public class MMOTPaymentTypeEntity : BaseEntity<int>
{
    public string? PaymentType { get; set; }
    public string? PaymentName { get; set; }
    public string? PaymentImg { get; set; }
    public bool IsActive { get; set; }
}

