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
    /// OrderItems Tablosu 
    /// </summary>
    [Table("OrderItems", Schema = "MarketPlace")]
    public class OrderItemEntity : BaseEntity<int>
    {
        public string OrderHeadId { get; set; }
        public int Article { get; set; }
        public string ArticleName { get; set; }
        public string orderLineNumber { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int? PgId { get; set; }
        public string? PgName { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatRate { get; set; }
        public string Barcode { get; set; }
        public int ItemStatus { get; set; }
        public int CancelledQty { get; set; }
        public int ReturnedQty { get; set; } 
        public int? UserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
