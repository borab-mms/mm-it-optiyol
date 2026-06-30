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
    /// OrderItemsStateQuantities Tablosu 
    /// </summary>
    [Table("OrderItemsStateQuantities", Schema = "FOM")]
    public class SterlingOrderItemsStateQuantityEntity : IEntity
    {

        [Key]
        public int Id { get; set; }

		//[ForeignKey("OrderHeadGuidId")]
		//public Guid OrderHeadGuidId { get; set; }
		public int CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public string State { get; set; }
        public string Quantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        //public virtual SterlingOrderHeadEntity OrderHead { get; set; }

    }
}
