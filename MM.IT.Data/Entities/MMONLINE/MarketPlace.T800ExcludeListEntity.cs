using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// T800ExcludeList Tablosu 
    /// </summary>
    [Table("T800ExcludeList", Schema = "MarketPlace")]
    public class T800ExcludeListEntity : BaseEntity<int>
    {
        public int? Article { get; set; }
        public string? ArticleName { get; set; }
        public int? PgId { get; set; }
        public string? PgName { get; set; }
        public bool? IsActive { get; set; }
        public int UserId { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
