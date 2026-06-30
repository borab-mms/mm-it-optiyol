using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// SMS_AutomaticContent Tablosu 
/// </summary>
[Table("SMS_AutomaticContent", Schema = "dbo")]
public class SMSAutomaticContentEntity : BaseEntityUpperCaseIDEntity<int>
{
    [Key]
    public int ID { get; set; }
    public string? MessageDescription { get; set; }
    public string? Mesgbody { get; set; }
    public string? Note { get; set; }
    public string? CreatedByRegistrationNo { get; set; }
    public string? ProjectName { get; set; }
    public string? VariableGroup { get; set; }
    public string? UpdatedByRegistrationNo { get; set; }
    public int Status { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

