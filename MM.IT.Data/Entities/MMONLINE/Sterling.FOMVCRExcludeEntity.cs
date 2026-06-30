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
    /// PGSMS Tablosu 
    /// </summary>
    [Table("VCRExcludes", Schema = "Sterling")]
    public class FOMVCRExcludeEntity : BaseEntity<int>
    {
        /// <summary>
        /// Article Bilgisi
        /// </summary>
        public int? Article { get; set; }

        /// <summary>
        /// PgId Bilgisi
        /// </summary>
        /// 
        public int? PgId { get; set; }

        /// <summary>
        /// IsActive Bilgisi
        /// </summary>
        /// 
        public bool IsActive { get; set; }

        /// <summary>
        /// UserId Bilgisi
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// UpdatedDate Bilgisi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
