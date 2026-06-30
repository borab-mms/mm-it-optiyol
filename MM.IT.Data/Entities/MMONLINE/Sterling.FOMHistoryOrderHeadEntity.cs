using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderHead Tablosu 
/// </summary>
[Table("HistoryOrderHead", Schema = "Sterling")]
public class FOMHistoryOrderHeadEntity : IEntity
{

    /// <summary>
    /// Id Bilgisi
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// SellerId Bilgisi
    /// </summary>
    public int? SellerId { get; set; }

    /// <summary>
    /// CooVersion Bilgisi
    /// </summary>
    public string? CooVersion { get; set; }
    /// <summary>
    /// IsAGT Bilgisi
    /// </summary>
    public bool? IsAGT { get; set; }
    /// <summary>
    /// IsAGTSuccess Bilgisi
    /// </summary>
    public bool? IsAGTSuccess { get; set; }

    /// <summary>
    /// SourceOms Bilgisi
    /// </summary>
    /// 
    public string? SourceOms { get; set; }

    /// <summary>
    /// OrderDate Bilgisi
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// OrderLanguage Bilgisi
    /// </summary>
    public string? OrderLanguage { get; set; }

    /// <summary>
    /// LastModified Bilgisi
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// CustomerType Bilgisi
    /// </summary>
    public string? CustomerType { get; set; }

    /// <summary>
    /// OrderStatus Bilgisi
    /// </summary>
    public string? OrderStatus { get; set; }

    /// <summary>
    /// CountryOrganization Bilgisi
    /// </summary>
    public string? CountryOrganization { get; set; }

    /// <summary>
    /// Brand Bilgisi
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// SalesChannel Bilgisi
    /// </summary>
    public string? SalesChannel { get; set; }

    /// <summary>
    /// OrderHoldFlag Bilgisi
    /// </summary>
    public bool? OrderHoldFlag { get; set; }

    /// <summary>
    /// HoldStatus Bilgisi
    /// </summary>
    public string? HoldStatus { get; set; }

    /// <summary>
    /// HoldType Bilgisi
    /// </summary>
    public string? HoldType { get; set; }

    /// <summary>
    /// HoldReason Bilgisi
    /// </summary>
    public string? HoldReason { get; set; }

    /// <summary>
    /// OrderValue Bilgisi
    /// </summary>
    public decimal? OrderValue { get; set; }

    /// <summary>
    /// OrderTaxValue Bilgisi
    /// </summary>
    public decimal? OrderTaxValue { get; set; }

    /// <summary>
    /// OrderTaxType Bilgisi
    /// </summary>
    public string? OrderTaxType { get; set; }

    /// <summary>
    /// OrderCurrency Bilgisi
    /// </summary>
    public string? OrderCurrency { get; set; }

    /// <summary>
    /// OrderMethod Bilgisi
    /// </summary>
    public string? OrderMethod { get; set; }

    /// <summary>
    /// ContractualPartner Bilgisi
    /// </summary>
    public int? ContractualPartner { get; set; }

    /// <summary>
    /// ReservationID Bilgisi
    /// </summary>
    public string? ReservationID { get; set; }

    /// <summary>
    /// CustomerUserId Bilgisi
    /// </summary>
    /// 
    public string? CustomerUserId { get; set; }

    /// <summary>
    /// OrderType Bilgisi
    /// </summary>
    public string? OrderType { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }

    /// <summary>
    /// Oluşturulma tarihi bilgisini saklar.
    /// </summary>
    public DateTime CreatedDate { get; set; }
}