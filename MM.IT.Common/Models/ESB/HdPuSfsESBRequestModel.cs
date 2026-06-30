using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.ESB
{
    public class HdPuSfsESBRequestModel
    {
        public string environment { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public OrderHeader orderHeader { get; set; }
        public List<OrderDetail> orderDetail { get; set; }
    }
    public class AddressInformation
    {
        public BillingAddress billingAddress { get; set; }
        public ShippingAddress shippingAddress { get; set; }
    }
    public class BillingAddress
    {
        public string id { get; set; }
        public string source { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string salutation { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
    public class Customer
    {
        public string id { get; set; }
        public string registrationType { get; set; }
        public string source { get; set; }
        public string vatRegNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public AddressInformation addressInformation { get; set; }
    }
    public class DeliveryDetails
    {
        public string deliveryType { get; set; }
        public string customerRequestedDeliveryType { get; set; }
        public string carrier { get; set; }
    }
    public class GrossTotal
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class GrossTotalExclTributes
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class NetTotal
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class Notes
    {
        public string noteText { get; set; }
        public string reasonCode { get; set; }
    }
    public class OrderDetail
    {
        public string lineItemNumber { get; set; }
        public string deliverySplitIndicator { get; set; }
        public string deliveryTimeCluster { get; set; }
        public DateTime reqShipDate { get; set; }
        public DateTime reqDeliveryDate { get; set; }
        public DateTime reservationNodeShipDate { get; set; }
        public bool reservationNodeShipDateSpecified { get; set; }
        public DateTime expectedDeliveryDate { get; set; }
        public bool expectedDeliveryDateSpecified { get; set; }
        public string subLineNo { get; set; }
        public string primeLineNo { get; set; }
        public string lineType { get; set; }
        public ProductInformation productInformation { get; set; }
        public List<OrderLineReservation> orderLineReservations { get; set; }
        public Quantities quantities { get; set; }
        public UnitPrice unitPrice { get; set; }
        public UnitPriceInclTributes unitPriceInclTributes { get; set; }
        public OriginalUnitPrice originalUnitPrice { get; set; }
        public OriginalUnitPriceInclTributes originalUnitPriceInclTributes { get; set; }
        public TotalLineItemPrice totalLineItemPrice { get; set; }
        public OriginalTotalLineItemPrice originalTotalLineItemPrice { get; set; }
        public UnitPriceNet unitPriceNet { get; set; }
        public UnitPriceNetInclTributes unitPriceNetInclTributes { get; set; }
        public TotalLineItemPriceNet totalLineItemPriceNet { get; set; }
        public TotalLineItemPriceNetInclTributes totalLineItemPriceNetInclTributes { get; set; }
        public VatPrice vatPrice { get; set; }
        public TotalLineItemVatPrice totalLineItemVatPrice { get; set; }
        public ShippingClassInformation shippingClassInformation { get; set; }
        public ShippingInformation shippingInformation { get; set; }
    }
    public class OrderHeader
    {
        public string orderNumber { get; set; }
        public string orderType { get; set; }
        public string outletId { get; set; }
        public string shippingCostCalculationType { get; set; }
        public Totals totals { get; set; }
        public DeliveryDetails deliveryDetails { get; set; }
        public Customer customer { get; set; }
        public PaymentInformation paymentInformation { get; set; }
        public string contractPartnerId { get; set; }
        public string taxId { get; set; }
        public bool emailFlag { get; set; }
        public Notes notes { get; set; }
    }
    public class OrderLineReservation
    {
        public DateTime requestedReservationDate { get; set; }
        public string shipNode { get; set; }
        public string itemId { get; set; }
        public string quantity { get; set; }
        public string reservationId { get; set; }
        public bool requestedReservationDateSpecified { get; set; }
        public string unitOfMeasure { get; set; }
    }
    public class OriginalTotal
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class OriginalTotalLineItemPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class OriginalUnitPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class OriginalUnitPriceInclTributes
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class PaymentInformation
    {
        public string type { get; set; }
        public string method { get; set; }
        public string payId { get; set; }
        public TransactionAmount transactionAmount { get; set; }
        public string status { get; set; }
        public string paymentServiceProviderName { get; set; }
        public string paymentServiceProviderId { get; set; }
    }
    public class ProductInformation
    {
        public string id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public double vatRateProduct { get; set; }
        public string vatKey { get; set; }
        public string itemDescription { get; set; }
        public string ean { get; set; }
    }
    public class Quantities
    {
        public string orderedQuantity { get; set; }
        [JsonIgnore]
        public string returnedQuantity { get; set; }
        [JsonIgnore]
        public string shippedQuantity { get; set; }
        [JsonIgnore]
        public string cancelledQuantity { get; set; }
        [JsonIgnore]
        public string replacedQuantity { get; set; }
        [JsonIgnore]
        public string availableQuantity { get; set; }
        [JsonIgnore]
        public string qtyFromUnplannedInventory { get; set; }
        public string unitOfMeasure { get; set; }
    }
    public class ShippingAddress
    {
        public string id { get; set; }
        public string source { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string salutation { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
    public class ShippingClassInformation
    {
        public string shippingCostClass { get; set; }
        public string shippingCostClassDistributionType { get; set; }
        public ShippingCostClassAmount shippingCostClassAmount { get; set; }
    }
    public class ShippingCostClassAmount
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class ShippingCosts
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }
    public class ShippingCostsNet
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }
    public class ShippingCostsVatPrice
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }
    public class ShippingInformation
    {
        public string shippingType { get; set; }
        public ShippingCosts shippingCosts { get; set; }
        public ShippingCostsNet shippingCostsNet { get; set; }
        public ShippingCostsVatPrice shippingCostsVatPrice { get; set; }
        public string vatKey { get; set; }
        public double vatRateShipping { get; set; }
    }
    public class TotalLineItemPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class TotalLineItemPriceNet
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class TotalLineItemPriceNetInclTributes
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class TotalLineItemVatPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class Totals
    {
        public NetTotal netTotal { get; set; }
        public GrossTotal grossTotal { get; set; }
        public GrossTotalExclTributes grossTotalExclTributes { get; set; }
        public OriginalTotal originalTotal { get; set; }
        public List<VatValue> vatValues { get; set; }
    }
    public class TransactionAmount
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class UnitPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class UnitPriceInclTributes
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class UnitPriceNet
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class UnitPriceNetInclTributes
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class VatPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }
    public class VatValue
    {
        public string vatKey { get; set; }
        public VatValue2 vatValue { get; set; }
        public double vatPercentage { get; set; }
    }
    public class VatValue2
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

}
