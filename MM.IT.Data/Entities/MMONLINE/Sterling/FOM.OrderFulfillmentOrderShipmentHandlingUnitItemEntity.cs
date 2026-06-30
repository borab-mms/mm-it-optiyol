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
    /// OrderFulfillmentOrderShipmentHandlingUnitItems Tablosu 
    /// </summary>
    [Table("OrderFulfillmentOrderShipmentHandlingUnitItems", Schema = "FOM")]
    public class SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderFulfillmentId { get; set; }
        public string OrderItemId { get; set; }
        public string ShipmentLineNumber { get; set; }
        public int? Quantity { get; set; }
        public string SerialNumbers { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
