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
    /// DC_Errors Tablosu 
    /// </summary>
    [Table("DC_Errors", Schema = "dbo")]
    public class DCErrorEntity : BaseEntity<int>
    {
        public int CustomerOrderNumber { get; set; }
        public string LineItemId { get; set; }
        public string ActionName { get; set; }
        public string ProcessName { get; set; }
        public string ResponseData { get; set; }
        public string RequestData { get; set; }
        public string ErrorMessage { get; set; }

    }
}
