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
/// SMS_OrderContentMatch Tablosu 
/// </summary>
[Table("SMS_OrderContentMatch", Schema = "dbo")]
public class SMSOrderContentMatchEntity : BaseEntityUpperCaseIDEntity<int>
{
    [Key]
    public int ID { get; set; }
    public string OrderType { get; set; }
    public string OrderMethod { get; set; }
    public string OrderStatus { get; set; }
    public int AutomaticContentId { get; set; }
    public string UserInfo { get; set; }
    public bool Status { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
