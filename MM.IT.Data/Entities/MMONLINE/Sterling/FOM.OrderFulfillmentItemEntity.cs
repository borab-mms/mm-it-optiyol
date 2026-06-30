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
    /// OrderFulfillmentItems Tablosu 
    /// </summary>
    [Table("OrderFulfillmentItems", Schema = "FOM")]
    public class SterlingOrderFulfillmentItemEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public string OrderFulfillmentId { get; set; }
        public string Quantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
