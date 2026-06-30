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
    [Table("PGSMS", Schema = "Sterling")]
    public class PGSMSEntity : BaseEntity<int>
    {
        /// <summary>
        /// PgId Bilgisi
        /// </summary>
        public int? PgId { get; set; }

        /// <summary>
        /// PgName Bilgisi
        /// </summary>
        /// 
        public string? PgName { get; set; }

        /// <summary>
        /// SMSAutomaticContentID Bilgisi
        /// </summary>
        /// 
        public int? SMSAutomaticContentID { get; set; }

        /// <summary>
        /// CreatedBy Bilgisi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedBy Bilgisi
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Status Bilgisi
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// UpdatedDate Bilgisi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
