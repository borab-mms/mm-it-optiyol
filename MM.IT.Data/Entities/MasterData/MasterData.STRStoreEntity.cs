using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MasterData;

/// <summary>
/// Master Data Entity -> Mağazalar Nesnesi
/// </summary>
[Table("Stores", Schema = "Store")]
public class MasterDataSTRStoreEntity : IEntity<int>, IDeactivableEntity, ISoftDeletableEntity
{
    /// <summary>
    /// Id Bilgisi
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// IsVCR Bilgisi
    /// </summary>
    public bool IsVCR { get; set; }

    /// <summary>
    /// VCRPrefix Bilgisi
    /// </summary>
    public string? VCRPrefix { get; set; }

    /// <summary>
    /// VCRStartValue Bilgisi
    /// </summary>
    public string? VCRStartValue { get; set; }

    /// <summary>
    /// Mağaza Sap Code Bilgisi
    /// </summary>
    public string SapCode { get; set; }

    /// <summary>
    /// Mağaza OutletId Bilgisi
    /// </summary>
    public int OutletId { get; set; }

    /// <summary>
    /// Mağaza WCSStoreId Bilgisi
    /// </summary>
    public int? WCSStoreId { get; set; }

    /// <summary>
    /// Mağaza Adı Bilgisi
    /// </summary>
    public string StoreName { get; set; }

    /// <summary>
    /// Email Bilgisi
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Açılış Tarihi
    /// </summary>
    public DateTime OpenDate { get; set; }

    /// <summary>
    /// Açılış Tarihi
    /// </summary>
    public DateTime? CloseDate { get; set; }

    /// <summary>
    /// Banka Adı Bilgisi
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// Banka Şubesi Bilgisi
    /// </summary>
    public string? BankBranch { get; set; }

    /// <summary>
    /// Iban Bilgisi
    /// </summary>
    public string? Iban { get; set; }

    /// <summary>
    /// Mağaza Tipi Id Bilgisi
    /// </summary>
    public int StoreTypeId { get; set; }

    ///// <summary>
    ///// Mağaza Kategori Bilgisi
    ///// </summary>
    //public string StoreCategory { get; set; }

    ///// <summary>
    ///// Bölge Id Bilgisi
    ///// </summary>
    //public int? RegionId { get; set; }

    ///// <summary>
    ///// HD Bölge Id Bilgisi
    ///// </summary>
    //public int? HDRegionId { get; set; }

    ///// <summary>
    ///// WKZ Bölge Id Bilgisi
    ///// </summary>
    //public int WKZRegionId { get; set; }

    /// <summary>
    /// Bölge Director No Bilgisi
    /// </summary>
    public string? RegionDirectorId { get; set; }

    /// <summary>
    /// Mağaza Yöneticisi No bilgisi
    /// </summary>
    public string? StoreManagerId { get; set; }

    /// <summary>
    /// Mağaza Adres Bilgisi
    /// </summary>
    public string StoreAddress { get; set; }

    /// <summary>
    /// Enlem Bilgisi
    /// </summary>
    public string? Latitude { get; set; }

    /// <summary>
    /// Boylam Bilgisi
    /// </summary>
    public string? Longitude { get; set; }

    /// <summary>
    /// Google Map Iframe Bilgisi
    /// </summary>
    public string? GoogleMapIframe { get; set; }

    /// <summary>
    /// Şehir Kodu
    /// </summary>
    public int CityCode { get; set; }

    /// <summary>
    /// İlçe Kodu
    /// </summary>
    public int DistrictCode { get; set; }

    /// <summary>
    /// Zip Kodu
    /// </summary>
    public int ZipCode { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// GLN ?
    /// </summary>
    public string? GLN { get; set; }

    /// <summary>
    /// EPS ?
    /// </summary>
    public bool? EPS { get; set; }

    /// <summary>
    /// WWSIP
    /// </summary>
    public string? WWSIP { get; set; }

    /// <summary>
    /// WWSIP 1
    /// </summary>
    public string? WWSIP1 { get; set; }

    /// <summary>
    /// WWSIP 2
    /// </summary>
    public string? WWSIP2 { get; set; }

    /// <summary>
    /// WWSIP 3
    /// </summary>
    public string? WWSIP3 { get; set; }

    /// <summary>
    /// WWSIP 4
    /// </summary>
    public string? WWSIP4 { get; set; }

    /// <summary>
    /// DBName
    /// </summary>
    public string? DBName { get; set; }

    /// <summary>
    /// DBName 1
    /// </summary>
    public string? DBName1 { get; set; }

    /// <summary>
    /// DBName 2
    /// </summary>
    public string? DBName2 { get; set; }

    /// <summary>
    /// İlgili Kaydın MPThreshold değeri
    /// </summary>
    public int MPThreshold { get; set; }    
    
    /// <summary>
    /// İlgili Kaydın Aktiflik durumu
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// İlgili Kaydın IsInMapper durumu
    /// </summary>
    public bool? IsInMapper { get; set; }

    /// <summary>
    /// İlgili Kaydın InMapperUrl durumu
    /// </summary>
    public string? InMapperUrl { get; set; }

    /// <summary>
    /// İlgili MPStatus durumu
    /// </summary>
    public bool MPStatus { get; set; }

    /// <summary>
    /// İlgili Kaydın Silinmişlik durumu
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// İlgili Kaydı Oluşturan Kişi NUmara Bilgisi
    /// </summary>
    public string CreatedByRegistrationNumber { get; set; }

    /// <summary>
    /// İlgili Kaydın Oluşturulma Tarihi Bilgisi
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// İlgili Kaydı Düzenleyen Kişi NUmara Bilgisi
    /// </summary>
    public string? UpdatedByRegistrationNumber { get; set; }

    /// <summary>
    /// İlgili Kaydın Düzenleme Tarihi Bilgisi
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}