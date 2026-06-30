using MM.IT.Data.Entities.MMOffline.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMOffline;
/// <summary>
/// WWS -> dbo.SFS_PICKUP_REPORTS
/// </summary>
[Table("SFS_PICKUP_REPORTS", Schema = "WWS")]
public class WWSSFSPickupReportEntity : IWWSEntityWithUpdatedDate
{
    /// <summary>
    /// Mağaza Kodu
    /// </summary>
    public string SAP_CODE { get; set; }

    /// <summary>
    /// ?
    /// </summary>
    public string TCODE { get; set; }

    /// <summary>
    /// ?
    /// </summary>
    public string TNAME { get; set; }

    /// <summary>
    /// Durum Bilgisi
    /// </summary>
    public string STAT { get; set; }

    /// <summary>
    /// Satış Türü
    /// </summary>
    public string SALES_TYP { get; set; }

    /// <summary>
    /// WWS Dosya No
    /// </summary>
    public int WWS_DOC_NO { get; set; }

    /// <summary>
    /// Oluşturma Tarihi
    /// </summary>
    public DateTime CREATE_TIME { get; set; }

    /// <summary>
    /// Güncelleme Tarihi
    /// </summary>
    public DateTime UPDATE_TIME { get; set; }

    /// <summary>
    /// Kapatma İptal Tarihi
    /// </summary>
    public DateTime? CLOSE_CANCEL_TIME { get; set; }

    /// <summary>
    /// Online Satış Doc No
    /// </summary>
    public int ONLINE_SALES_DOC_NO { get; set; }

    /// <summary>
    /// Artikel No
    /// </summary>
    public int ART_NO { get; set; }

    /// <summary>
    /// Artikel Açıklaması
    /// </summary>
    public string ART_DESC { get; set; }

    /// <summary>
    /// Departman No
    /// </summary>
    public int? DEPT_NO { get; set; }

    /// <summary>
    /// Departman Adı
    /// </summary>
    public string DEPT_NAME { get; set; }

    /// <summary>
    /// Ürün Grup No
    /// </summary>
    public int PG { get; set; }

    /// <summary>
    /// Ürün Grup Adı
    /// </summary>
    public string PG_NAME { get; set; }

    /// <summary>
    /// Adet ?
    /// </summary>
    public int QTY { get; set; }

    /// <summary>
    /// Fiyat ?
    /// </summary>
    public decimal VAL { get; set; }

    //Extra Columns
    /// <summary>
    /// Güncellenme Tarihi - Kaynak Veri
    /// </summary>
    public DateTime INTEGRATION_UPDATED_DATE { get; set; }
}