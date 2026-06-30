using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// PaymentItem Tablosu 
/// </summary>
[Table("PaymentItems", Schema = "MEX")]
public class MEXPaymentItemEntity : BaseEntity<int>
{
    public int ItemId { get; set; }
    public string OrderId { get; set; }
    public string ScheduledPaymentInfo { get; set; }
    public string MerchantPaymentItem { get; set; }
    public string CustomerDebtItemInfo { get; set; }
    public string BasketItemId { get; set; }
    public string Name { get; set; }
    public string Category1 { get; set; }
    public string Category2 { get; set; }
    public string Category3 { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string CriticalCategory { get; set; }
    public string ProductId { get; set; }
    public string ItemType { get; set; }
}