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
    /// Master Data DB -> İlçe Entity Nesnesi
    /// </summary>
    [Table("ZC_Districts", Schema = "dbo")]
    public class MasterDataZCDistrictEntity : IEntity<int>
    {
        /// <summary>
        /// ID Bilgisi -> ILCE_ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// İlçe Adı Bilgisi -> ADI
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// İlçe Kodu Bilgisi -> UAVTKODU
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
        /// Şehir Id Bilgisi -> IL_ID
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Şehir Erişim Nesnesi
        /// </summary>
        public virtual MasterDataZCCityEntity City { get; set; }

    }
}
