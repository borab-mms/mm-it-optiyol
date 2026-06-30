using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData
{
    /// <summary>
    /// Master Data DB -> Artikel Departman Entity Nesnesi
    /// </summary>
    [Table("ART_Department", Schema = "dbo")]
    public class MasterDataARTDepartmentEntity : IEntity
    {
        /// <summary>
        /// Departman Id Bilgisi
        /// </summary>
        public short DpId { get; set; }

        /// <summary>
        /// Departman Adı Bilgisi
        /// </summary>
        public string DpName { get; set; }

        /// <summary>
        /// Domain Id Bilgisi
        /// </summary>
        public int DomainId { get; set; }
    }
}
