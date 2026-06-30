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
    /// SFS_Borusan_Head Tablosu 
    /// </summary>
    [Table("SFS_Borusan_Head", Schema = "dbo")]
    public class SFSBorusanHeadEntity:IEntity
    {
        [Key]
        public string OrderId { get; set; }
        public string orderNumber { get; set; }
        public string StoreId { get; set; }
        public int SalesDocument { get; set; }
        public string trackingUrl { get; set; }
        public string orderDetailDeliveryStatus { get; set; }
        public DateTime? estimatedDeliveryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<SFSBorusanStatusEntity> SFSBorusanStatus { get; set; }
    }
}
