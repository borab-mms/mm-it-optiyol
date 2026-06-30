using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT
{
    /// <summary>
    ///  AyenSoftLog Tablosu 
    /// </summary>
    [Table("AyenSoftLog", Schema = "dbo")]
    public class AyenSoftLogEntity : BaseEntity<int>
    {
        public string? Request { get; set; }
        public string? Response { get; set; }
        public DateTime? SendingDate { get; set; }
    }
}
