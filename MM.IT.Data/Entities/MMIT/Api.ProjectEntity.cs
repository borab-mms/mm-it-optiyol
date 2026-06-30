using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT
{
    /// <summary>
    /// Projects Tablosu 
    /// </summary>
    [Table("Projects", Schema = "Api")]
    public class ApiProjectEntity : BaseEntity<int>
    {
        public string ProjectName { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
