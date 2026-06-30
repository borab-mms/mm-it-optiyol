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
    /// OrderUpdateHistory Tablosu 
    /// </summary>
    [Table("OrderUpdateHistory", Schema = "MarketPlace")]
    public class OrderUpdateHistoryEntity : BaseEntity<int>
    {
        public int? CustomerOrderNumber { get; set; }
        public string? OrderHeadId { get; set; }
        public string? OrderPackageNumber { get; set; }
        public int? Article { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public string? StatusType { get; set; }

    }
}
