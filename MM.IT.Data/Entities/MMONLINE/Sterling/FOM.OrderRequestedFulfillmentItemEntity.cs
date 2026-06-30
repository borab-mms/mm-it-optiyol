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
    /// OrderRequestedFulfillmentItems Tablosu 
    /// </summary>
    [Table("OrderRequestedFulfillmentItems", Schema = "FOM")]
    public class SterlingOrderRequestedFulfillmentItemEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public Guid? OrderRequestedFulfillmentId { get; set; }
        public string OrderItemId { get; set; }
        public string Quantity { get; set; }
        public string SourceNodeId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
