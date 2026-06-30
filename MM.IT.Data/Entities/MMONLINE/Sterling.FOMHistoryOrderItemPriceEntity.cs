using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// HistoryOrderItemPrices Tablosu 
/// </summary>
[Table("HistoryOrderItemPrices", Schema = "Sterling")]
public class FOMHistoryOrderItemPriceEntity : BaseEntity<int>
{

    /// <summary>
    /// GrossAmount Bilgisi
    /// </summary>
    /// 
    public decimal? GrossAmount { get; set; }
    /// <summary>
    /// CustomerOrderNumber Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// ProductId Bilgisi
    /// </summary>
    /// 
    public string ProductId { get; set; }

    /// <summary>
    /// LineItemId Bilgisi
    /// </summary>
    /// 
    public string LineItemId { get; set; }

    /// <summary>
    /// LineItemReference Bilgisi
    /// </summary>
    /// 
    public string LineItemReference { get; set; }

    /// <summary>
    /// LineItemStatusDescription Bilgisi
    /// </summary>
    /// 
    public string LineItemStatusDescription { get; set; }

    /// <summary>
    /// GrossCurrency Bilgisi
    /// </summary>
    public string? GrossCurrency { get; set; }

    /// <summary>
    /// NetAmount Bilgisi
    /// </summary>
    public decimal? NetAmount { get; set; }

    /// <summary>
    /// NetCurrency Bilgisi
    /// </summary>
    public string? NetCurrency { get; set; }

    /// <summary>
    /// VatAmount Bilgisi
    /// </summary>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// VatCurrency Bilgisi
    /// </summary>
    public string? VatCurrency { get; set; }

    /// <summary>
    /// RetailUnitPriceGrossAmount Bilgisi
    /// </summary>
    public decimal? RetailUnitPriceGrossAmount { get; set; }

    /// <summary>
    /// RetailUnitPriceGrossCurrency Bilgisi
    /// </summary>
    public string? RetailUnitPriceGrossCurrency { get; set; }

    /// <summary>
    /// RetailUnitPriceNetAmount Bilgisi
    /// </summary>
    public decimal? RetailUnitPriceNetAmount { get; set; }

    /// <summary>
    /// RetailUnitPriceNetCurrency Bilgisi
    /// </summary>
    public string? RetailUnitPriceNetCurrency { get; set; }

    /// <summary>
    /// RetailUnitPriceVatAmount Bilgisi
    /// </summary>
    public decimal? RetailUnitPriceVatAmount { get; set; }

    /// <summary>
    /// RetailUnitPriceVatCurrency Bilgisi
    /// </summary>
    public string? RetailUnitPriceVatCurrency { get; set; }


    /// <summary>
    /// GroupKey tarihi bilgisini saklar.
    /// </summary>
    public string? GroupKey { get; set; }


}