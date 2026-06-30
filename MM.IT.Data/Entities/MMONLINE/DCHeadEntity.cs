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
    /// DC_Head Tablosu 
    /// </summary>
    [Table("DC_Head", Schema = "dbo")]
    public class DCHeadEntity : BaseEntity<int>
    {
        public int CustomerOrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int? CustomerUserId { get; set; }
        public string CustomerSalutation { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
