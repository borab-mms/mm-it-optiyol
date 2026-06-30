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
    /// OrderRequestedFulfillments Tablosu 
    /// </summary>
    [Table("OrderRequestedFulfillments", Schema = "FOM")]
    public class SterlingOrderRequestedFulfillmentEntity : IEntity
    {

        [Key]
        public Guid GuidId { get; set; }
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string Type { get; set; }
        public string LevelOfservice { get; set; }
        public bool? ShipFromStore { get; set; }
        public string PickupLocationType { get; set; }
        public string PickupLocationOutletId { get; set; }
        public string PickupLocationOutletName { get; set; }
        public string PickupLocationBusinessPartnerId { get; set; }
        public string DeliveryPromiseEarliest { get; set; }
        public string DeliveryPromiseLatest { get; set; }
        public string DeliveryPromiseDisplayText { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
