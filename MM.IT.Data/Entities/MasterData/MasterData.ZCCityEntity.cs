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
    /// Master Data DB -> İl Entity Nesnesi
    /// </summary>
    [Table("ZC_Cities", Schema = "dbo")]
    public class MasterDataZCCityEntity : IEntity<int>
    {
        /// <summary>
        /// ID Bilgisi -> IL_ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Şehir Adı Bilgisi -> ADI
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Şehir Kodu Bilgisi -> UAVTKODU
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Boylam Bilgisi -> LONGX
        /// </summary>
        public decimal Longitute { get; set; }

        /// <summary>
        /// Enlem Bilgisi -> LATY
        /// </summary>
        public decimal Latitute { get; set; }

        /// <summary>
        /// Populasyon -> NUFUS
        /// </summary>
        public int Population { get; set; }

        /// <summary>
        /// Ülke Id Bilgisi -> ULKE_ID
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// İlçeler Erişim Nesnesi
        /// </summary>
        public virtual ICollection<MasterDataZCDistrictEntity> Districts { get; set; }
    }
}
