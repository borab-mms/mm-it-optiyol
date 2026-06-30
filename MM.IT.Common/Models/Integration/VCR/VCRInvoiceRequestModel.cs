using MM.IT.Common.Models.ESB;
using MM.IT.Common.Models.Sterling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MM.IT.Common.Models.Integration.VCR
{
    public class VCRInvoiceRequestModel
    {
        public DocumentHeader DocumentHeader { get; set; }
        public Customer Customer { get; set; }
        public SalesLines SalesLines { get; set; }
        public VatFooter VatFooter { get; set; }
        public TotalDiscounts TotalDiscounts { get; set; }
        public Total Total { get; set; }
        public Payments Payments { get; set; }
        public Text Text { get; set; }
        //public Metadata Metadata { get; set; }
    }
    public class DocumentHeader
    {

        public string InvoiceId { get; set; }
        public string Uuid { get; set; }
        public int? SystemNo { get; set; }
        public string OutletSAPCode { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceChannel { get; set; }
        public int? CashregisterNo { get; set; }
        public int? CashierNo { get; set; }
        public int? DrawerNo { get; set; }
        public string IssueDate { get; set; }
        public string IssueTime { get; set; }
        public int? LineCount { get; set; }
    }
    public class Customer
    {
        public long? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string FamilyOrCompanyName { get; set; }
        public string StreetName { get; set; }
        public string CitySubdivisionName { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string TaxOffice { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public bool? EMailFlag { get; set; }
        public string LoyaltyCustomerText { get; set; }
        public string LoyaltyCustomerNumber { get; set; }
    }
    public class Products
    {
        public int? LineNo { get; set; }
        public int? Article { get; set; }
        public string ArticleName { get; set; }
        public int? Quantity { get; set; }
        public decimal? OriginalPrice { get; set; }
        //public decimal? Discounts { get; set; }
        public List<Discount> Discounts { get; set; }
        public Metadata Metadata { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? RetailPriceTotalAmount { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? VatAmount { get; set; }
        public int? ProductGroupNo { get; set; }
        public string ProductGroupName { get; set; }
        public string ManufacturerName { get; set; }
        public string SerialNo { get; set; }
        public decimal? RetailNetPrice { get; set; }
        public decimal? RetailNetPriceTotalAmount { get; set; }
        public decimal? VatTotalAmount { get; set; }
        public bool? IsThirdPartyProduct { get; set; }
    }
    public class ListOfSalesDocItemLines
    {
        public List<Products> Products { get; set; }
    }
    public class SalesDocument
    {
        public int? LineNo { get; set; }
        public int? Number { get; set; }
        public decimal? RetailPriceTotalAmount { get; set; }
        public ListOfSalesDocItemLines ListOfSalesDocItemLines { get; set; }
    }
    public class SalesLines
    {
        public SalesDocument SalesDocument{ get; set; }
    }
    public class VatLine
    {
        public decimal? InvoiceNetAmount { get; set; }
        public decimal? InvoiceVatAmount { get; set; }
        public decimal? InvoiceVatRate { get; set; }
    }
    public class Discount
    {
        public string DiscountIdentifier { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public int? DiscountTypeFlavors { get; set; }
    }
    public class ListOfVatLines
    {
        public VatLine VatLine { get; set; }
    }
    public class VatFooter
    {
        public ListOfVatLines ListOfVatLines { get; set; }
    }
    public class TotalDiscounts
    {
        public decimal? TotalDiscountAmount { get; set; }
        public string TotalDiscountAmountInWords { get; set; }
        public decimal? TotalDiscountNetAmount { get; set; }
        public decimal? TotalDiscountVatAmount { get; set; }
    }
    public class Total
    {
        public decimal? InvoiceTotalAmount { get; set; }

        public string InvoiceAmountInWords { get; set; }
        public decimal? InvoiceTotalNetAmount { get; set; }
        public decimal? InvoiceTotalVatAmount { get; set; }
    }
    public class PaymentLine
    {
        public string PaymentChannelType { get; set; }
        public decimal? PaymentChannelAmount { get; set; }
        public string PaymentChannelDetail { get; set; }
    }
    public class ListOfPaymentLines
    {
        public PaymentLine PaymentLine { get; set; }
    }
    public class Payments
    {
        public ListOfPaymentLines ListOfPaymentLines { get; set; }
        public decimal CashChange { get; set; }
    }
    public class Text
    {

        public string LegalText { get; set; }
        public string InvoiceTypeText { get; set; }
        public string PrinterCode { get; set; }
        public string Notes { get; set; }
    }
    public class ListOfWarrantyRefLine
    {
        public WarrantyRefLine warrantyRefLine { get; set; }
    }

    public class Metadata
    {
        public List<ListOfWarrantyRefLine> listOfWarrantyRefLines { get; set; }
    }

    public class Root
    {
        public Metadata metadata { get; set; }
    }

    public class WarrantyRefLine
    {
        public string warrantySlipNumber { get; set; }
        public string infoText { get; set; }
    }

}
