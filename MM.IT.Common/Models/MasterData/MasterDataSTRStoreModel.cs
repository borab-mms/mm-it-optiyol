using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MasterData
{
    /// <summary>
    /// Master Data Store Model Nesnesi
    /// </summary>
    public class MasterDataSTRStoreModel
    {
        /// <summary>
        /// Id Bilgisi
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mağaza Sap Code Bilgisi
        /// </summary>
        public string SapCode { get; set; }

        /// <summary>
        /// Mağaza OutletId Bilgisi
        /// </summary>
        public int OutletId { get; set; }

        /// <summary>
        /// Mağaza Adı Bilgisi
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// MPThreshold
        /// </summary>
        public int MPThreshold { get; set; }

        /// <summary>
        /// Mağaza Adres Bilgisi
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// Şehir Kodu
        /// </summary>
        public int? CityCode { get; set; }

        /// <summary>
        /// İlçe Kodu
        /// </summary>
        public int? DistrictCode { get; set; }

        /// <summary>
        /// Zip Kodu
        /// </summary>
        public int? ZipCode { get; set; }

        /// <summary>
        /// İlgili Kaydın Aktiflik durumu
        /// </summary>
        public bool IsActive { get; set; }
    }
}
