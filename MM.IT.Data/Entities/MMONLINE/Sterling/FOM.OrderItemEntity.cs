using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE.Sterling
{
    /// <summary>
    /// OrderItems Tablosu 
    /// </summary>
    [Table("OrderItems", Schema = "FOM")]
    public class SterlingOrderItemEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
		[ForeignKey("OrderHeadGuidId")]
		public Guid OrderHeadGuidId { get; set; }
		public int CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public string? ExternalItemId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderedQuantity { get; set; }
        public string? Ean { get; set; }
        public string? LogisticClassRef { get; set; }
        public string? Manufacturer { get; set; }
        public string? HandlingType { get; set; }
        public string? Type { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? VatSing { get; set; }
        public string? VatRate { get; set; }
        public string? KindOfProduct { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
		public virtual SterlingOrderHeadEntity OrderHead { get; set; }

	}
}
