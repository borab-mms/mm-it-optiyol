using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// MEX_RawDatas Tablosu 
/// </summary>
[Table("PaymentHeader", Schema = "MEX")]
public class MEXPaymentHeaderEntity : BaseEntity<int>
{
    public string Action { get; set; }
    public string CompanyInfoCode { get; set; }
    public string CompanyInfoMerchantKey { get; set; }
    public string PaymentResult { get; set; }
    public string ProcessInfoOrderId { get; set; }
    public string ProcessInfoMerchantProcessId { get; set; }
    public string ProcessInfoMerchantCustomField { get; set; }
    public string ProcessInfoChannel { get; set; }
    public decimal ProcessInfoTotalAmountRequested { get; set; }
    public decimal ProcessInfoTotalAmountProcessed { get; set; }
    public bool ProcessInfoIsLoggedInProcess { get; set; }
    public string ProcessInfoPaymentDescription { get; set; }
    public string ProcessInfoGroupCode { get; set; }
    public string ProcessInfoOtpVerificationType { get; set; }
    public string ProcessInfoPaymentConsentsPaymentExtraConsent1 { get; set; }
    public string ProcessInfoPaymentConsentsPaymentExtraConsent2 { get; set; }
    public string CustomerInfoEmail { get; set; }
    public string CustomerInfoCustomerId { get; set; }
    public string CustomerInfoName { get; set; }
    public string CustomerInfoPhone { get; set; }
    public string CustomerInfoCustomVal1 { get; set; }
    public string CustomerInfoCustomVal2 { get; set; }
    public string CustomerInfoCustomVal3 { get; set; }
    public string CustomerInfoTcknVkn { get; set; }
    public string CustomerInfoCustomerGroupName { get; set; }
    public string Result { get; set; }
    public string CardNum { get; set; }
    public string CardToken { get; set; }
    public string OldCardNum { get; set; }
    public string OldCardToken { get; set; }
    public string CardHolderName { get; set; }
    public string CardBankCode { get; set; }
    public string CardBankName { get; set; }
    public string CardProgram { get; set; }
    public string CardHolderNameFromBank { get; set; }
    public string CardSaveExtraConsent1 { get; set; }
}