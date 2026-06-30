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
    /// SFS_Borusan_Status Tablosu 
    /// </summary>
    [Table("SFS_Borusan_Status", Schema = "dbo")]
    public class SFSBorusanStatusEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("OrderId")]
        public string OrderId { get; set; }
        public string orderDetailDeliveryStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual SFSBorusanHeadEntity SFSBorusanHead { get; set; }
    }
}
