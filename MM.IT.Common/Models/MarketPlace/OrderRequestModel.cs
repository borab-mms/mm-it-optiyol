using FluentValidation;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class OrderRequestModel
    {
        public OrderHeader OrderHeader { get; set; }
        public Customer Customer { get; set; }
        public List<ProductInformation> Products { get; set; }
    }
    public class AddressInformation
    {
        public BillingAddress billingAddress { get; set; }
        public ShippingAddress shippingAddress { get; set; }
    }
    public class BillingAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string mobileNumber { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public string township { get; set; }
        public string district { get; set; }
        public string companyName { get; set; }
        public string country { get; set; }
        public bool isEinvoice { get; set; }
        public string taxOffice { get; set; }
        public string taxNo { get; set; }
        public string tcKimlikNo { get; set; }
    }
    public class Customer
    {
        public int customerId { get; set; }
        public AddressInformation addressInformation { get; set; }
    }
    public class OrderHeader
    {
        public int customerOrderNumber { get; set; }
        public string orderHeadId { get; set; }
        public string IntegratorOrderNumber { get; set; }
        public string channelCode { get; set; }
        public string channelOrderNumber { get; set; }
        public string channelPackageNumber { get; set; }
        public DateTime channelOrderDate { get; set; }
        public string customerType { get; set; }
        public string channelOrderStatus { get; set; }
        public string shippingCompany { get; set; }
        public DateTime shippingDue { get; set; }
        public string cargoCode { get; set; }
        public string currencyCode { get; set; }
        public string channelShipmentType { get; set; }
        public string channelOrderNote { get; set; }
        public decimal? grossTotal { get; set; }
        public decimal? totalNetPrice { get; set; }
        public decimal? totalVatAmount { get; set; }
        public string? orderType { get; set; }
        public string? sapCode { get; set; }
        public int? outletId { get; set; }
        public int? whId { get; set; }
        public string? deliveryType { get; set; }
        public string? customerRequestedDeliveryType { get; set; }
        public string? carrier { get; set; }
        public string? outletName { get; set; }
        public string? outletAddress { get; set; }
        public string? outletZip { get; set; }
        public string? outletCity { get; set; }
        public string? outletDistrict { get; set; }
    }
    public class ProductInformation
    {
        public int article { get; set; }
        public string name { get; set; }
        public string orderLineNumber { get; set; }
        public int quantity { get; set; }
        public decimal amount { get; set; }
        public int discount { get; set; }
        public int vatKey { get; set; }
        public decimal vatRate { get; set; }
        public string barcode { get; set; }
        public decimal? price { get; set; }
        public decimal? netPrice { get; set; }
        public decimal? vatAmount { get; set; }
        public int? pgId { get; set; }
        public string? pgName { get; set; }
        public string? brand { get; set; }
        public string? itemId { get; set; }
    }
    public class ShippingAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string mobileNumber { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
        public string township { get; set; }
        public string district { get; set; }
        public string tcKimlikNo { get; set; }
        public string country { get; set; }
        public string taxNo { get; set; }
    }

    /// <summary>
    /// Fluent Validateion Modeli
    /// </summary>
    public class OrderRequestModelValidator : AbstractValidator<OrderRequestModel>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderRequestModelValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(p => p.OrderHeader.IntegratorOrderNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.OrderHeader.channelCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.OrderHeader.channelOrderNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.OrderHeader.channelPackageNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.OrderHeader.channelOrderDate)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.OrderHeader.channelOrderStatus)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.OrderHeader.shippingCompany)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.OrderHeader.shippingDue)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.OrderHeader.channelShipmentType)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(p => p.OrderHeader.cargoCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.OrderHeader.currencyCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

            //RuleFor(p => p.OrderHeader.channelOrderNote)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(500);

            RuleFor(p => p.Customer.addressInformation.billingAddress.address)
             //.NotNull()
             //.NotEmpty()
             .MaximumLength(150);

            RuleFor(p => p.Customer.addressInformation.billingAddress.city)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.billingAddress.township)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.billingAddress.district)
                .MaximumLength(35);

            RuleFor(p => p.Customer.addressInformation.billingAddress.zipCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => p.Customer.addressInformation.billingAddress.mobileNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.billingAddress.firstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Customer.addressInformation.billingAddress.lastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            //RuleFor(p => p.Customer.addressInformation.billingAddress.companyName)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(100);

            RuleFor(p => p.Customer.addressInformation.billingAddress.country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => p.Customer.addressInformation.billingAddress.email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);

            //RuleFor(p => p.Customer.addressInformation.billingAddress.isEinvoice)
            //    .NotNull()
            //    .NotEmpty();

            //RuleFor(p => p.Customer.addressInformation.billingAddress.taxOffice)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(35);

            //RuleFor(p => p.Customer.addressInformation.billingAddress.taxNo)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(25);

            //RuleFor(p => p.Customer.addressInformation.billingAddress.tcKimlikNo)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(11);

            //RuleFor(p => p.Customer.addressInformation.shippingAddress.address)
            //    .NotNull()
            //    .NotEmpty()
            //    .MaximumLength(75);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.city)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.township)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.district)
                .MaximumLength(35);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.zipCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.mobileNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.firstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Customer.addressInformation.shippingAddress.lastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Products).NotEmpty().ForEach(x => x.SetValidator(new ProductInformationValidator()));

        }
    }

    /// <summary>
    /// Fluent Validateion Modeli
    /// </summary>
    public class ProductInformationValidator : AbstractValidator<ProductInformation>, IPmModelValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductInformationValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            RuleFor(x => x.article)
             .NotNull()
              .GreaterThanOrEqualTo(1000000)
              .LessThanOrEqualTo(9999999);

            RuleFor(p => p.name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(p => p.orderLineNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.quantity)
             .NotNull()
              .GreaterThanOrEqualTo(0)
              .LessThanOrEqualTo(99);

            RuleFor(p => p.amount)
             .NotNull()
              .GreaterThanOrEqualTo(0);

            RuleFor(p => p.discount)
             .NotNull()
              .GreaterThanOrEqualTo(0);

            RuleFor(p => p.vatRate)
             .NotNull()
              .GreaterThan(0)
              .LessThanOrEqualTo(999);

            RuleFor(p => p.barcode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(35);

        }
    }
}
