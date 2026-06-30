using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.FOM
{
    /// <summary>
    /// Fom Data DB -> Arvato_Head Entity Nesnesi
    /// </summary>
    [Table("Arvato_Head", Schema = "dbo")]
    public class FomArvatoHeadEntity : IEntity
    {
        [Key]
        public string orderNumber { get; set; }
        public string orderId { get; set; }
        public string customerOrderNumber { get; set; }
        public string customerReferenceNumber { get; set; }
        public string carrierAccount { get; set; }
        public string account { get; set; }
        public string trackingUrl { get; set; }
        public string cargoKey { get; set; }
        public string carrierAccountCode { get; set; }
        public string orderTypeCode { get; set; }
        public string eDespatchNo { get; set; }
        public string deliveryStatus { get; set; }
        public DateTime? actualShipDate { get; set; }
        public DateTime? cargoSentDate { get; set; }
        public DateTime? cargoDeliveryDate { get; set; }
    }
}
