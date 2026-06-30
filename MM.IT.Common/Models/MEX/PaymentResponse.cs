using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MEX
{
    public class PaymentResponseTest
    {
        public string action { get; set; }
    }
    public class PaymentResponse
    {
        public string action { get; set; }
        public CompanyInfo companyInfo { get; set; }
        public string paymentResult { get; set; }
        public ProcessInfo processInfo { get; set; }
        public CustomerInfo customerInfo { get; set; }
        public List<BasketItem> basketItems { get; set; }
        public List<PaymentList> paymentList { get; set; }
    }
    public class ScheduledPaymentInfo
    {
        public DueDate dueDate { get; set; }
    }
    public class MerchantPaymentItem
    {
    }
    public class CompanyInfo
    {
        public string code { get; set; }
        public string merchantKey { get; set; }
    }
    public class ProcessInfo
    {
        [Required]
        public string orderId { get; set; }
        public string merchantProcessId { get; set; }
        public string merchantCustomField { get; set; }
        public string channel { get; set; }
        public decimal totalAmountRequested { get; set; }
        public decimal totalAmountProcessed { get; set; }
        public bool isLoggedInProcess { get; set; }
        public PaymentConsents paymentConsents { get; set; }
        public string paymentDescription { get; set; }
        public string groupCode { get; set; }
        public string otpVerificationType { get; set; }
    }
    public class PaymentConsents
    {
        public object paymentExtraConsent1 { get; set; }
        public object paymentExtraConsent2 { get; set; }
    }
    public class CustomerInfo
    {
        public string email { get; set; }
        public string customerId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string customVal1 { get; set; }
        public string customVal2 { get; set; }
        public string customVal3 { get; set; }
        public string tcknVkn { get; set; }
        public string customerGroupName { get; set; }
    }
    public class BasketItem
    {
        public ScheduledPaymentInfo scheduledPaymentInfo { get; set; }
        public MerchantPaymentItem merchantPaymentItem { get; set; }
        public CustomerDebtItemInfo customerDebtItemInfo { get; set; }
        public int basketItemId { get; set; }
        public string name { get; set; }
        public string category1 { get; set; }
        public string category2 { get; set; }
        public string category3 { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public string criticalCategory { get; set; }
        public string itemIdGivenByMerchant { get; set; }
        public string itemType { get; set; }
    }
    public class AcquirerResultDetail
    {
        public Pos pos { get; set; }
        public BankAccount bankAccount { get; set; }
        public EmoneyAccount emoneyAccount { get; set; }
    }
    public class BankAccount
    {
    }
    public class Card
    {
        public string processType { get; set; }
        public string refTransType { get; set; }
        public string maskedCardNumber { get; set; }
        public string cardNamebyUser { get; set; }
        public string cardBankCode { get; set; }
        public string cardBankName { get; set; }
        public string cardProgram { get; set; }
        public string cardHolderName { get; set; }
        public string cardToken { get; set; }
        public bool isCardSaved { get; set; }
        public bool is3DSecure { get; set; }
        public bool isDebitCard { get; set; }
        public bool isBusinessCard { get; set; }
        public bool isMoto { get; set; }
        public string cardNoMode { get; set; }
    }
    public class CustomerDebtItemInfo
    {
        public DueDate dueDate { get; set; }
    }
    public class DueDate
    {
    }
    public class Emoney
    {
    }
    public class EmoneyAccount
    {
    }
    public class FraudControlInfo
    {
        public string fraudControlResult { get; set; }
        public bool isAutoCancelled { get; set; }
        public string externalFraudControlResult { get; set; }
    }
    public class InstrumentDetail
    {
        public Card card { get; set; }
        //public WireTransfer wireTransfer { get; set; }
        //public Emoney emoney { get; set; }
    }
    public class PaybackTransactionList
    {
    }
    public class PaymentList
    {
        public string orderId { get; set; }
        public bool isSuccess { get; set; }
        public bool isCancelled { get; set; }
        public string systemTransId { get; set; }
        public DateTime transactionDate { get; set; }
        public decimal amountRequested { get; set; }
        public decimal amountProcessed { get; set; }
        public decimal usedLoyaltyPoint { get; set; }
        public int installmentCount { get; set; }
        public int extraInstallment { get; set; }
        public string currency { get; set; }
        public string instrumentType { get; set; }
        public InstrumentDetail instrumentDetail { get; set; }
        public string acquirerType { get; set; }
        public AcquirerResultDetail acquirerResultDetail { get; set; }
        public ResultCategory resultCategory { get; set; }
        public List<PaybackTransactionList> paybackTransactionList { get; set; }
        public FraudControlInfo fraudControlInfo { get; set; }
        public bool isPostauthed { get; set; }
        public bool isRefunded { get; set; }
        public bool isFullRefunded { get; set; }
        public int totalRefundedAmount { get; set; }
    }
    public class Pos
    {
        public int posId { get; set; }
        public string posBankCode { get; set; }
        public string posBankName { get; set; }
        public string cardHolderNameFromBank { get; set; }
        public string cardProcessingType { get; set; }
        public string returnCode { get; set; }
        public string message { get; set; }
        public DateTime hostDate { get; set; }
        public string authCode { get; set; }
        public string transId { get; set; }
        public string referenceNo { get; set; }
        public string customData { get; set; }
    }
    public class ResultCategory
    {
        public string categoryCode { get; set; }
        public string categoryName { get; set; }
        public string recommendedUIMessageTR { get; set; }
        public string recommendedUIMessageEN { get; set; }
    }
    public class WireTransfer
    {
    }
}
