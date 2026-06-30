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
    /// SMS_Errors Tablosu 
    /// </summary>
    [Table("SMS_Errors", Schema = "dbo")]
    public class SMSErrorEntity : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string ActionName { get; set; }
        public string SourceName { get; set; }
        public string ProcessName { get; set; }
        public string JsonData { get; set; }
        public string RequestData { get; set; }
        public string RequestDate { get; set; }
        public string ResponseData { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
