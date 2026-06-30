using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// MEX_OrderHead Tablosu 
/// </summary>
[Table("OrderHead", Schema = "MEX")]
public class MEXOrderHeadEntity : BaseEntity<int>
{
    public string CompanyInfoMerchantKey { get; set; }
    public string? PaymentResult { get; set; }
    public string? ProcessInfoOrderId { get; set; }
    public decimal? ProcessInfoTotalAmountRequested { get; set; }
    public decimal? ProcessInfoTotalAmountProcessed { get; set; }
    public bool? ProcessInfoIsLoggedInProcess { get; set; }
    public string? CustomerInfoEmail { get; set; }
    public string? CustomerInfoCustomerId { get; set; }
    public string? CustomerInfoName { get; set; }
    public string? CustomerInfoPhone { get; set; }
    public string? ResultCategoryCategoryCode { get; set; }
}
