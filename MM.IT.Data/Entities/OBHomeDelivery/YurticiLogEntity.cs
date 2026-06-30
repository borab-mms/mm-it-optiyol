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
    /// Yurtici_Log Tablosu 
    /// </summary>
    [Table("Yurtici_Log", Schema = "dbo")]
    public class YurticiLogEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Key]
        public DateTime changeDate { get; set; }
        public string deliveryDate { get; set; }
        public string deliveryTime { get; set; }
        public string estimatedDeliveryDate { get; set; }
        public string cargoEventId { get; set; }
        public string cargoEventExplanation { get; set; }
        public string cargoReasonId { get; set; }
        public string cargoReasonExplanation { get; set; }
    }
}
