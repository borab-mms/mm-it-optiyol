using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData
{
    /// <summary>
    /// Master Data DB -> Mahalle Entity Nesnesi
    /// </summary>
    [Table("ZC_Neighborhoods", Schema = "dbo")]
    public class MasterDataZCNeighborhoodEntity : IEntity<int>
    {
        /// <summary>
        /// ID Bilgisi -> ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Mahalle Adı Bilgisi -> ADI
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Boylam Bilgisi -> LONGX
        /// </summary>
        public decimal Longitute { get; set; }

        /// <summary>
        /// Enlem Bilgisi -> LATY
        /// </summary>
        public decimal Latitute { get; set; }

        /// <summary>
        /// Tip Bilgisi -> TIP
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Posta Kodu Bilgisi -> POSTAKODU
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// İlçe Id Bilgisi -> ILCE_ID
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// İlçe Erişim Nesnesi
        /// </summary>
        public virtual MasterDataZCDistrictEntity District { get; set; }
    }
}
