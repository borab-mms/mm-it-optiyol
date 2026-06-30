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
    /// OrderFulfillmentOrderShipments Tablosu 
    /// </summary>
    [Table("OrderFulfillmentOrderShipments", Schema = "FOM")]
    public class SterlingOrderFulfillmentOrderShipmentEntity : IEntity
    {

        [Key]
        public int Id { get; set; }
        public int CustomerOrderNumber { get; set; }
        public string OrderFulfillmentId { get; set; }
        public string State { get; set; }
        public string ShippedAt { get; set; }
        public string ExpectedDeliveryTime { get; set; }
        public string PickedUp { get; set; }
        public string NumberOfReminder { get; set; }
        public string OrderShipmentId { get; set; }
        public string Type { get; set; }
        public string ShipmentNode { get; set; }
        public string CustomerInvoiceId { get; set; }
        public string GrossAmount { get; set; }
        public string GrossAmountCurrency { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

    }
}
