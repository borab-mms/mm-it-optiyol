using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.OBHomeDelivery
{
    /// <summary>
    /// Kartur_Order_Link Tablosu 
    /// </summary>
    [Table("Kartur_Order_Link", Schema = "dbo")]
    public class KarturOrderLinkEntity : IEntity
    {
        [Key]
        public string ReferenceId { get; set; }
        public string ShortLink { get; set; }
        public string StoreId { get; set; }
        public int SalesDocument { get; set; }
        public string OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
