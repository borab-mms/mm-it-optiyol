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
    /// SMS_Channels Tablosu 
    /// </summary>
    [Table("SMS_Channels", Schema = "dbo")]
    public class SMSChannelEntity : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
