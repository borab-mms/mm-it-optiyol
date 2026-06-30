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
/// SMS_SendingLOG Tablosu 
/// </summary>
[Table("SMS_SendingLOG", Schema = "dbo")]
public class SMSSendingLOGEntity : BaseEntityUpperCaseIDEntity<int>
{
    [Key]
    public int ID { get; set; }
    public string? OrderNo { get; set; }
    public string? GsmNo { get; set; }
    public string? ClubCardNo { get; set; }
    public int? AutomaticContentID { get; set; }
    public int? SMSProcessID { get; set; }
    public DateTime? SendDate { get; set; }
}
