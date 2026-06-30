using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.OBHomeDelivery
{
    /// <summary>
    /// Yurtici_Head Tablosu 
    /// </summary>
    [Table("Yurtici_Head", Schema = "dbo")]
    public class YurticiHeadEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string SenderStoreId { get; set; }
        public string SourceId { get; set; }
        public string trackingUrl { get; set; }
        public string docId { get; set; }
        public string cargoEventExplanation { get; set; }
        public string deliveryDate { get; set; }
        public string deliveryTime { get; set; }
        public string estimatedDeliveryDate { get; set; }
        public DateTime? PrintedOn { get; set; }
    }
}
