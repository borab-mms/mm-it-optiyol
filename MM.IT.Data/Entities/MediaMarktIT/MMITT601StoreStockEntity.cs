using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT
{

    /// <summary>
    /// WWS -> T601 ağaza Stok Verisi
    /// </summary>
    [Table("T601Store_Stocks", Schema = "dbo")]
    public class MMITT601StoreStockEntity : IEntity
    {
        /// <summary>
        /// Mağaza Kodu Bilgisi
        /// </summary>
        public string SAP_CODE { get; set; }

        /// <summary>
        /// Artikel Numarası Bilgisi
        /// </summary>
        public int Article { get; set; }

        /// <summary>
        /// Stok Depo Id Bilgisi
        /// </summary>
        public int WarehouseID { get; set; }

        /// <summary>
        /// Stok Adedi Bilgisi
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Rezerve Stok Adedi Bilgisi
        /// </summary>
        public int ReservationQuantity { get; set; }

        /// <summary>
        /// Güncelleme Tarihi -> Entegrasyondan En Son Alınma Tarihi.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Oluşma Tarihi -> Entegrasyondan ilk alış.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
