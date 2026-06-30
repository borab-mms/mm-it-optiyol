using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// Products Tablosu 
/// </summary>
[Table("Products", Schema = "VCR")]
public class VCRProductEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public int? SalesDocId { get; set; }
    public int? LineNo { get; set; }
    public int? Article { get; set; }
    public string? ArticleName { get; set; }
    public bool? InvoiceDisplayFlag { get; set; }
    public int? Quantity { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal? RetailPrice { get; set; }
    public decimal? RetailPriceTotalAmount { get; set; }
    public decimal? VatRate { get; set; }
    public decimal? VatAmount { get; set; }
    public int? PGID { get; set; }
    public string? ManufacturerName { get; set; }
    public string? SerialNo { get; set; }
    public decimal? RetailNetPrice { get; set; }
    public decimal? RetailNetPriceTotalAmount { get; set; }
    public decimal? VatTotalAmount { get; set; }
    public bool? IsThirdPartyProduct { get; set; }
}

