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
    /// OrderFulfillments Tablosu 
    /// </summary>
    [Table("OrderFulfillments", Schema = "FOM")]
    public class SterlingOrderFulfillmentEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderFulfillmentId { get; set; }
        public string Type { get; set; }
        public string LevelOfservice { get; set; }
        public bool? ShipFromStore { get; set; }
        public string SourceNodeId { get; set; }
        public string ExternalSalesDocumentId { get; set; }
        public string PickupLocationType { get; set; }
        public string PickupLocationOutletId { get; set; }
        public string PickupLocationBusinessPartnerId { get; set; }
        public string ShipmentId { get; set; }
        public string CarrierCode { get; set; }
        public string ServiceCode { get; set; }
        public string DeliveryPromiseEarliest { get; set; }
        public string DeliveryPromiseLatest { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }

}
