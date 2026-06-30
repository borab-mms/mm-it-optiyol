using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT
{
    /// <summary>
    /// SMS_Successfull Tablosu 
    /// </summary>
    [Table("SMS_Successfull", Schema = "dbo")]
    public class SMSSuccessfullEntity : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string ProcessId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
