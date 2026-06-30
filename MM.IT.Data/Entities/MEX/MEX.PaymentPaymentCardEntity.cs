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
[Table("PaymentPaymentCards", Schema = "MEX")]
public class MEXPaymentPaymentCardEntity : BaseEntity<int>
{
    public int PaymentId { get; set; }
    public int CardId { get; set; }
    public string ProcessType { get; set; }
    public string RefTransType { get; set; }
    public string MaskedCardNumber { get; set; }
    public string CardNamebyUser { get; set; }
    public string CardBankCode { get; set; }
    public string CardBankName { get; set; }
    public string CardProgram { get; set; }
    public string CardHolderName { get; set; }
    public string CardToken { get; set; }
    public bool IsCardSaved { get; set; }
    public bool Is3DSecure { get; set; }
    public bool IsDebitCard { get; set; }
    public bool IsBusinessCard { get; set; }
    public bool IsMoto { get; set; }
    public string CardNoMode { get; set; }
}