using MM.IT.Data.Entities.Base;
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
    /// SMS_AutomaticContent Tablosu 
    /// </summary>
    [Table("Email_Contents", Schema = "dbo")]
    public class EmailContentEntity : BaseEntityUpperCaseIDEntity<int>
    {
        [Key]
        public int ID { get; set; }
        public string EmailDescription { get; set; }
        public string EmailContent { get; set; }
        public bool IsActive { get; set; }
    }
}
