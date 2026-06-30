using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMIT;

/// <summary>
/// SalesDocs Tablosu 
/// </summary>
[Table("SalesDocs", Schema = "VCR")]
public class VCRSalesDocEntity : BaseEntity<int>
{
    public string InvoiceId { get; set; }
    public int? LineNo { get; set; }
    public int? SalesDocNumber { get; set; }
    public decimal? RetailPriceTotalAmount { get; set; }
}

