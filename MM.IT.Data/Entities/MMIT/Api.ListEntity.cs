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
    /// ApiList Tablosu 
    /// </summary>
    [Table("ApiList", Schema = "Api")]
    public class ApiListEntity : BaseEntity<int>
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string ApiName { get; set; }

        public string ApiLink { get; set; }

        public string NamespacePath { get; set; }

        public string AssemblyName { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? UpdatedBy  { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
