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
    /// OrderFulfillmentOrderShipmentHandlingUnits Tablosu 
    /// </summary>
    [Table("OrderFulfillmentOrderShipmentHandlingUnits", Schema = "FOM")]
    public class SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderFulfillmentId { get; set; }
        public string OrderShipmentId { get; set; }
        public string CarrierCode { get; set; }
        public string TrackingId { get; set; }
        public string TrackingLinkUrl { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
