using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// ESB Tablosu 
    /// </summary>
    [Table("ESB", Schema = "MarketPlace")]
    public class ESBEntity : BaseEntity<int>
    {
        public string Request { get; set; }
        public string Response { get; set; }

    }
}
