using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// MEX_PaymentPayments Tablosu 
/// </summary>
[Table("PaymentPayments", Schema = "MEX")]
public class MEXPaymentPaymentEntity : BaseEntity<int>
{
    public int PaymentId { get; set; }
    public string OrderId { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsCancelled { get; set; }
    public string SystemTransId { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal AmountRequested { get; set; }
    public decimal AmountProcessed { get; set; }
    public string UsedLoyaltyPoint { get; set; }
    public int InstallmentCount { get; set; }
    public int ExtraInstallment { get; set; }
    public string Currency { get; set; }
    public string InstrumentType { get; set; }
    public string AcquirerType { get; set; }
    public string CategoryCode { get; set; }
    public string CategoryName { get; set; }
    public string RecommendedUIMessageTR { get; set; }
    public string RecommendedUIMessageEN { get; set; }
    public string FraudControlResult { get; set; }
    public bool IsAutoCancelled { get; set; }
    public string ExternalFraudControlResult { get; set; }
}