using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// WWS -> Mağaza Stok Verisi
/// </summary>
[Table("Store_Stocks", Schema = "dbo")]
public class MMITStoreStockEntity : IEntity
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
    /// ?
    /// </summary>
    public string ArticleOID { get; set; }

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
    /// Oluşma Tarihi -> Entegrasyondan ilk alış.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Güncelleme Tarihi -> Entegrasyondan En Son Alınma Tarihi.
    /// </summary>
    public DateTime UpdatedDate { get; set; }

    //Extra Columns
    /// <summary>
    /// Güncellenme Tarihi - Kaynak Veri
    /// </summary>
    public DateTime INTEGRATION_UPDATED_DATE { get; set; }
}