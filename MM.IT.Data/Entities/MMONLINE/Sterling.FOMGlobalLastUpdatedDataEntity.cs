using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// GlobalLastUpdatedData Tablosu 
    /// </summary>
    [Table("GlobalLastUpdatedData", Schema = "Sterling")]
    public class FOMGlobalLastUpdatedDataEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? SourceName { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModifiedFrom { get; set; }
    }
}
