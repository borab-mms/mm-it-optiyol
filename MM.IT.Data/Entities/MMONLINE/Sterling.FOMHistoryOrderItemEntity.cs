using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderItems Tablosu 
/// </summary>
[Table("HistoryOrderItems", Schema = "Sterling")]
public class FOMHistoryOrderItemEntity : BaseEntity<int>
{   
    
    /// <summary>
    /// OrderItemID Bilgisi
    /// </summary>
    public string? OrderItemID { get; set; }

    /// <summary>
    /// LineİtemID Bilgisi
    /// </summary>
    public string? LineItemID { get; set; }

    /// <summary>
    /// LineİtemReference Bilgisi
    /// </summary>
    /// 
    public string? LineItemReference { get; set; }

    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// LineİtemStatusDescription Bilgisi
    /// </summary>
    public string? LineItemStatusDescription { get; set; }

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
    /// ProductSerialNumber Bilgisi
    /// </summary>
    public string? ProductSerialNumber { get; set; }

    /// <summary>
    /// SerialNumbers Bilgisi
    /// </summary>
    public string? SerialNumbers { get; set; }

    /// <summary>
    /// ProductID Bilgisi
    /// </summary>
    public string? ProductID { get; set; }

    /// <summary>
    /// ProductName Bilgisi
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// Manufacturer Bilgisi
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// ProductPrice Bilgisi
    /// </summary>
    public decimal? ProductPrice { get; set; }

    /// <summary>
    /// OriginalProductPrice Bilgisi
    /// </summary>
    public decimal? OriginalProductPrice { get; set; }

    /// <summary>
    /// TotalQuantity Bilgisi
    /// </summary>
    public int? TotalQuantity { get; set; }

    /// <summary>
    /// İtemPrice Bilgisi
    /// </summary>
    public decimal? İtemPrice { get; set; }

    /// <summary>
    /// LogisticsClass Bilgisi
    /// </summary>
    public string? LogisticsClass { get; set; }

    /// <summary>
    /// VatSign Bilgisi
    /// </summary>
    public int? VatSign { get; set; }

    /// <summary>
    /// VatPercentage Bilgisi
    /// </summary>
    public string? VatPercentage { get; set; }

    /// <summary>
    /// QuantityOrdered Bilgisi
    /// </summary>
    public decimal? QuantityOrdered { get; set; }

    /// <summary>
    /// QuantityShipped Bilgisi
    /// </summary>
    public decimal? QuantityShipped { get; set; }

    /// <summary>
    /// QuantityReturned Bilgisi
    /// </summary>
    public decimal? QuantityReturned { get; set; }

    /// <summary>
    /// QuantityCancelled Bilgisi
    /// </summary>
    public decimal? QuantityCancelled { get; set; }

    /// <summary>
    /// QuantityReplaced Bilgisi
    /// </summary>
    public decimal? QuantityReplaced { get; set; }

    /// <summary>
    /// Position Bilgisi
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// ExpectedDeliveryDate Bilgisi
    /// </summary>
    public DateTime? ExpectedDeliveryDate { get; set; }

    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }
}