using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// ArchiveCheck Tablosu 
    /// </summary>
    [Table("ArchiveCheck", Schema = "Sterling")]
    public class FOMArchiveCheckEntity : IEntity<int>
    {
        /// <summary>
        /// Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DataDate Bilgisi
        /// </summary>
        public DateTime DataDate { get; set; }

        /// <summary>
        /// CountTransaction Bilgisi
        /// </summary>
        /// 
        public int? CountTransaction { get; set; }

        /// <summary>
        /// TotalData Bilgisi
        /// </summary>
        /// 
        public int? TotalData { get; set; }

        /// <summary>
        /// Result Bilgisi
        /// </summary>
        public int? Result { get; set; }

        /// <summary>
        /// StartDate Bilgisi
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// EndDate Bilgisi
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// UpdatedDate Bilgisi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
