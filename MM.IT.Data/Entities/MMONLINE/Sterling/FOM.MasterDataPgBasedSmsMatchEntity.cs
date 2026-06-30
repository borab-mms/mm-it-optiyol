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
    /// MasterDataPgBasedSmsMatch Tablosu 
    /// </summary>
    [Table("MasterDataPgBasedSmsMatch", Schema = "FOM")]
    public class SterlingMasterDataPgBasedSmsMatchEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? PgId { get; set; }
        public int? SMSAutomaticContentId { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
    }
}
