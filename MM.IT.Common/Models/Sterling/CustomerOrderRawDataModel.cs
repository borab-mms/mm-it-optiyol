using MM.IT.Common.Models.PIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
	{
		public List<AddressComponent> address_components { get; set; }
	}

	public class AddressComponent
	{
		public string? CustomerOrderNumber { get; set; }
		public AddresseeComponent AddresseeComponent { get; set; }
		public StreetComponent StreetComponent { get; set; }
		public CityComponent CityComponent { get; set; }
		public CompanyComponent CompanyComponent { get; set; }
		public EmailComponent EmailComponent { get; set; }
		public AddressSelectorComponent AddressSelectorComponent { get; set; }
		public CustomerComponent CustomerComponent { get; set; }
		public PhoneComponent PhoneComponent { get; set; }
	}	
	public class AddressComponent2
	{
		public string? CustomerOrderNumber { get; set; }
        public string AddressType { get; set; }
        public string Type { get; set; }
        public List<AddressComponent2Item> AddressComponent2Items { get; set; }

    }	
	public class AddressComponent2Item
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }
	public class AddresseeComponent
	{
		public string type { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
	}
	public class StreetComponent
	{
		public string type { get; set; }
		public string street { get; set; }
		public string house_number { get; set; }
		public string unit { get; set; }
		public string level { get; set; }
	}
	public class CityComponent
	{
		public string type { get; set; }
		public string zipcode { get; set; }
		public string city { get; set; }
		public string country { get; set; }
	}
	public class CompanyComponent
	{
		public string type { get; set; }
		public string company_name { get; set; }
		public string tax_id { get; set; }
		public string vat_id { get; set; }
	}
	public class EmailComponent
	{
		public string type { get; set; }
		public string email { get; set; }
	}
	public class AddressSelectorComponent
	{
		public string district { get; set; }
		public string quarter { get; set; }
		public string quarter_id { get; set; }
	}
	public class CustomerComponent
	{
		public string tax_id { get; set; }
		public string phone_number { get; set; }
		public string quarter_id { get; set; }
		public string ciid { get; set; }
	}
	public class PhoneComponent
	{
		public string phone_number { get; set; }
	}

	public class Amount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
		//public GrossAmount gross_amount { get; set; }
		//public NetAmount net_amount { get; set; }
		//public VatAmount vat_amount { get; set; }
	}

	public class Article
	{
		public string ean { get; set; }
		public string logistic_class_ref { get; set; }
		public List<Feature> features { get; set; }
		public Manufacturer manufacturer { get; set; }
		public insured_amount insured_amount { get; set; }
		public int? contract_period { get; set; }
		public int? good_will_period { get; set; }
		public bool? external { get; set; }
		public string general_terms_and_conditions { get; set; }
		public string certificate_number { get; set; }
		public string handling_type { get; set; }
		public string type { get; set; }
		public int? id { get; set; }
		public string name { get; set; }
		public Vat vat { get; set; }
		public string kind_of_product { get; set; }

	}
	public class Manufacturer
	{
		public string name { get; set; }
	}
	public class insured_amount
    {
		public string amount { get; set; }
		public string currency { get; set; }
	}

	public class BillingAddress
	{
		public AddressComponent address_components { get; set; }
	}

	public class Cancellation
	{
		public string state { get; set; }
		public string created { get; set; }
		public string updated { get; set; }
		public string reason { get; set; }
		public string order_management_system_response { get; set; }
	}

	public class Carrier
	{
		public string carrier_code { get; set; }
		public string service_code { get; set; }
		public string earliest { get; set; }
		public string latest { get; set; }
	}

	public class Charge
	{
		public RefundAmount refund_amount { get; set; }
		public string shipping_cost_type { get; set; }
		public string item_id { get; set; }
	}

	public class ChildItem
	{
		public string value { get; set; }
	}

	public class ContractualPartner
	{
		public string id { get; set; }
		public string sap_code { get; set; }
		public string name { get; set; }
		//public Address address { get; set; }
		public ContractualPartnerAddress contractualPartnerAddress { get; set; }
		public string business_partner_id { get; set; }
		public string operation_type { get; set; }
	}

	public class Customer
	{
		public string id { get; set; }
		public string party_id { get; set; }
		public bool? guest { get; set; }
	}

	public class CustomerCost
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
	}

	public class CustomerOrder
	{
		public string id { get; set; }
		public string rawDataId { get; set; }
		public bool isNewOrder { get; set; }
		public bool isVCR { get; set; }
		public bool isVCRCheck { get; set; }
		public bool isT800MP { get; set; }
		public string SellerType { get; set; }
		public bool isPimCheck { get; set; }
		public bool isBoCheck { get; set; }
		public bool isShippedCheck { get; set; }
		public bool isSendToFspCheck { get; set; }
		public bool isMasterDataStatusList { get; set; }
		public string? orderMethod { get; set; }
		public int? SellerId { get; set; }
		public string SellerName { get; set; }
		public string number { get; set; }
		public string country { get; set; }
		public string language { get; set; }
		public string sales_line { get; set; }
		public SourceSystem source_system { get; set; }
		public string state { get; set; }
		public string business_relationship { get; set; }
		public Customer customer { get; set; }
		public AddressComponent billing_address_temp { get; set; }
		public List<AddressComponent2> billing_address_temp_2 { get; set; }
		public Totals01 totals { get; set; }
		public DateTime? created_at { get; set; }
		public DateTime? updated_at { get; set; }
		public string kind { get; set; }
		public ContractualPartner contractual_partner { get; set; }
		public Payment payment { get; set; }
		public MmsOrder mms_order { get; set; }
		public List<MarketplaceOrder> marketplace_orders { get; set; }
		public string predecessor_customer_order_id { get; set; }
	}

	public class CustomerTotal
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public List<VatAmount10> vat_amounts { get; set; }
	}
	public class DeliveryPromise
	{
		public string? earliest { get; set; }
		public string? latest { get; set; }
		public string display_text { get; set; }
		public string order_item_id { get; set; }
		public string quantity { get; set; }
	}
	public class Fulfillment
	{
		public string id { get; set; }
		public string type { get; set; }
		public List<FulfillmentItem> items { get; set; }
		public string level_of_service { get; set; }
		public bool? ship_from_store { get; set; }
		public PickupLocation pickup_location { get; set; }
		public string source_node_id { get; set; }
		public DeliveryPromise delivery_promise { get; set; }
		public Shipment shipment { get; set; }
		public List<OrderShipment> order_shipments { get; set; }
		public string external_sales_document_id { get; set; }
	}
	public class FulfillmentItem
	{
		public string order_item_id { get; set; }
		public string? quantity { get; set; }
	}
	public class GrandTotal01
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}
	public class GrandTotal
	{
		//public string? amount { get; set; }
		//public string currency { get; set; }
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public List<VatAmount10> vat_amounts { get; set; }
	}

	public class GrossAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class HandlingUnit
	{
		public List<Item> items { get; set; }
		public string carrier_code { get; set; }
		public string tracking_id { get; set; }
		public string tracking_link_url { get; set; }
	}

	public class Hold
	{
		public string type { get; set; }
		public string state { get; set; }
		public string reason { get; set; }
	}

	public class Item
	{
		public string order_item_id { get; set; }
		public int? quantity { get; set; }
		public int? ordered_quantity { get; set; }
		public List<StateQuantity> state_quantities { get; set; }
		public Article article { get; set; }
		public Prices prices { get; set; }
		public string external_item_id { get; set; }
		public List<PriceAdjustments> price_adjustments { get; set; }
		public List<ChildItem> child_items { get; set; }
		public string predecessor_item_id { get; set; }
		public List<Hold> holds { get; set; }
		public List<HoldsTemp> holdsTemp { get; set; }
		public string aggregated_state { get; set; }
		public ShippingCostsDetail shipping_costs_detail { get; set; }
		public string source_node_id { get; set; }
		public string? shipment_line_number { get; set; }
		public List<string> serial_numbers { get; set; }
		public RefundAmounts refund_amounts { get; set; }
		public string id { get; set; }
		public string offer_id { get; set; }
		public OriginalPrice original_price { get; set; }
		public OriginalUnitPrice original_unit_price { get; set; }
		public RetailPrice retail_price { get; set; }
		public RetailUnitPrice retail_unit_price { get; set; }
		public ShippingCosts shipping_costs { get; set; }
		public string product_id { get; set; }
		public string product_name { get; set; }
	}

	public class ItemRefundAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class MarketplaceOrder
	{
		public string id { get; set; }
		public string state { get; set; }
		public Seller seller { get; set; }
		public List<Item> items { get; set; }
		public Totals totals { get; set; }
		public ShippingAddress shipping_address { get; set; }
		public string carrier_code { get; set; }
		public string tracking_id { get; set; }
		public string tracking_url { get; set; }
	}

	public class MmsOrder
	{
		public string state { get; set; }
		public List<Item> items { get; set; }
		public Totals totals { get; set; }
		public List<RequestedFulfillment> requested_fulfillments { get; set; }
		public bool? is_cancellable { get; set; }
		public List<Hold> holds { get; set; }
		public AddressComponent shipping_address { get; set; }
		public List<Fulfillment> fulfillments { get; set; }
		public List<Return> returns { get; set; }
		public Cancellation cancellation { get; set; }
		public string aggregated_state { get; set; }
		public List<Note> notes { get; set; }
	}

	public class NetAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class Note
	{
		public string id { get; set; }
		public Ref @ref { get; set; }
		public string text { get; set; }
		public string reason { get; set; }
		public bool? @internal { get; set; }
		public bool? high_priority { get; set; }
		public string email { get; set; }
		public string phone_number { get; set; }
		public string? created_by { get; set; }
		public string? created_at { get; set; }
	}

	public class OrderShipment
	{
		public string id { get; set; }
		public string type { get; set; }
		public string state { get; set; }
		public string shipped_at { get; set; }
		public string expected_delivery_time { get; set; }
		public string picked_up { get; set; }
		public string number_of_reminder { get; set; }
		public List<HandlingUnit> handling_units { get; set; }
		public string shipment_node { get; set; }
		public string customer_invoice_id { get; set; }
		public GrossAmount gross_amount { get; set; }
	}

	public class OriginalItemPrice
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
	}

	public class OriginalPrice
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class OriginalUnitPrice
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class Outlet
	{
		public string id { get; set; }
		public string sap_code { get; set; }
		public string name { get; set; }
		public Address address { get; set; }
		public string business_partner_id { get; set; }
		public string operation_type { get; set; }
	}

	public class Payment
	{
		public string id { get; set; }
		public List<PaymentPart> payment_parts { get; set; }
	}

	public class PaymentPart
	{
		public string method { get; set; }
		public Amount amount { get; set; }
		public string pay_id { get; set; }
		public string merchant_id { get; set; }
        public string card_brand { get; set; }
        public string gift_card_set_id { get; set; }
        public string gift_card_in_set_id { get; set; }
    }

	public class PickupLocation
	{
		public string type { get; set; }
		public Outlet outlet { get; set; }
	}

	public class PriceAdjustment
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class PriceAdjustment2
	{
		public string id { get; set; }
		public string promotionType { get; set; }
		public string type { get; set; }
		public string name { get; set; }
		public string cost_center { get; set; }
		public string discount_class { get; set; }
		public GrossAmount gross_amount { get; set; }

	}

	public class Prices
	{
		public OriginalUnitPrice original_unit_price { get; set; }
		public RetailItemPrice retail_item_price { get; set; }
		public RetailUnitPrice retail_unit_price { get; set; }
		public OriginalItemPrice original_item_price { get; set; }
	}

	public class Ref
	{
		public string type { get; set; }
		public string value { get; set; }
	}

	public class RefundAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class RefundAmounts
	{
		public UnitRefundAmount unit_refund_amount { get; set; }
		public ItemRefundAmount item_refund_amount { get; set; }
	}

	public class RequestedFulfillment
	{
		public string type { get; set; }
		public List<RequestedFulfillmentItem> items { get; set; }
		public string level_of_service { get; set; }
		public bool? ship_from_store { get; set; }
		public PickupLocation pickup_location { get; set; }
		public DeliveryPromise delivery_promise { get; set; }
	}
	public class RequestedFulfillmentItem
	{
		public string order_item_id { get; set; }
		public string quantity { get; set; }
		public string? source_node_id { get; set; }
	}

	public class RetailItemPrice
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
	}

	public class RetailPrice
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class RetailUnitPrice
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class Return
	{
		public string id { get; set; }
		public string refunde_in_store { get; set; }
		public string externa_sales_document_id { get; set; }
		public List<ReturnItem> items { get; set; }
		public List<Charge> charges { get; set; }
		public string State { get; set; }
	}
	public class ReturnItem
	{
		public string order_item_id { get; set; }
		public Article article { get; set; }
		public string quantity { get; set; }
		public string return_reason { get; set; }
		public string customer_return_reason { get; set; }
		public RefundAmounts refund_amounts { get; set; }

	}
	public class CustomerOrderModel
	{
		public List<CustomerOrder> customer_orders { get; set; }
		public List<PriceAdjustments> price_adjustments { get; set; }
		public List<AddressComponent> address_components { get; set; }
		public List<AddressComponent2> address_components2 { get; set; }
		public List<AddressComponent> address_componentsForshipping_address { get; set; }
		public List<ContractualPartnerAddress> contractualPartnerAddresss { get; set; }
        public List<HoldsTemp> holdsTemp { get; set; }
        public int offset { get; set; }
		public int limit { get; set; }
		public bool has_next { get; set; }
		public int? next_offset { get; set; }
	}
	public class Seller
	{
		public string id { get; set; }
		public string name { get; set; }
	}

	public class Shipment
	{
		public string id { get; set; }
		public string service_code { get; set; }
		public Carrier carrier { get; set; }
	}

	public class ShippingAddress
	{
		public AddressComponent address_component { get; set; }
	}

	public class ShippingCosts
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class ShippingCostsDetail
	{
		public CustomerCost customer_cost { get; set; }
		public ShippingFeeCost shipping_fee_cost { get; set; }
		public List<Surcharge> surcharges { get; set; }
	}

	public class ShippingCostsTotal
	{
		public CustomerTotal customer_total { get; set; }
		public ShippingFeeTotal shipping_fee_total { get; set; }
		public SurchargeTotal surcharge_total { get; set; }
	}

	public class ShippingFeeCost
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public VatAmount vat_amount { get; set; }
	}

	public class ShippingFeeTotal
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public List<VatAmount10> vat_amounts { get; set; }
	}

	public class SourceSystem
	{
		public string channel { get; set; }
		public string id { get; set; }
		public string type { get; set; }
	}

	public class StateQuantity
	{
		public string state { get; set; }
		public string? quantity { get; set; }
	}

	public class SubTotal
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public List<VatAmount10> vat_amounts { get; set; }
	}

	public class Surcharge
	{
		public Amount amount { get; set; }
		public string type { get; set; }
	}

	public class SurchargeTotal
	{
		public GrossAmount gross_amount { get; set; }
		public NetAmount net_amount { get; set; }
		public List<VatAmount10> vat_amounts { get; set; }
	}

	public class Totals01
	{
		public GrandTotal01 grand_total { get; set; }
		public ShippingCosts shipping_costs { get; set; }
		public PriceAdjustment price_adjustment { get; set; }
		public ShippingCostsTotal shipping_costs_total { get; set; }
		public SubTotal sub_total { get; set; }
	}
	public class Totals
	{
		public GrandTotal grand_total { get; set; }
		public ShippingCosts shipping_costs { get; set; }
		public PriceAdjustment price_adjustment { get; set; }
		public ShippingCostsTotal shipping_costs_total { get; set; }
		public SubTotal sub_total { get; set; }
	}

	public class UnitRefundAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class Vat
	{
		public string? sign { get; set; }
		public string? rate { get; set; }
	}

	public class VatAmount
	{
		public string? amount { get; set; }
		public string currency { get; set; }
	}

	public class VatAmount10
	{
		public Amount amount { get; set; }
		public Vat vat { get; set; }
	}

	public class Promotion
	{
		public string Id { get; set; }
		public string PromotionType { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
	}	
	public class FreeShipping
	{
		public string Id { get; set; }
		public string PromotionType { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
	}
	public class Coupon
	{
		public string Id { get; set; }
		public string CostCenter { get; set; }
		public string DiscountClass { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public GrossAmount GrossAmount { get; set; }
	}	
	public class ItemDiscount
	{
		public string Id { get; set; }
		public string CostCenter { get; set; }
		public string DiscountClass { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public GrossAmount GrossAmount { get; set; }
	}
	public class PriceAdjustments
	{
        public string Id { get; set; }
        public string PromotionType { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }  // Optional, only present in the second object
        public string DiscountClass { get; set; } // Optional, only present in the second object
        public GrossAmount GrossAmount { get; set; } // Opt
        public string CustomerOrderNumber { get; set; }
		public string OrderItemId { get; set; }
		public Promotion Promotion { get; set; }
		public Coupon Coupon { get; set; }
	}
	public class ContractualPartnerAddress
	{
		public string CustomerOrderNumber { get; set; }
		public StreetComponentForContractualPartner StreetComponentForContractualPartner { get; set; }
		public CityComponentForContractualPartner CityComponentForContractualPartner { get; set; }
		public CompanyComponentForContractualPartner CompanyComponentForContractualPartner { get; set; }
	}
	public class StreetComponentForContractualPartner
	{
		public string type { get; set; }
		public string street { get; set; }
		public string house_number { get; set; }
	}
	public class CityComponentForContractualPartner
	{
		public string type { get; set; }
		public string zipcode { get; set; }
		public string city { get; set; }
		public string country { get; set; }
	}
	public class CompanyComponentForContractualPartner
	{
		public string type { get; set; }
		public string company_name { get; set; }
		public string tax_id { get; set; }
	}
    public class HoldsTemp
    {
        public string CustomerOrderNumber { get; set; }
        public string OrderItemId { get; set; }
        public AWAITINGSALESDOCComponent AWAITINGSALESDOCComponent { get; set; }
        public AWAITINGSHIPMENTIDComponent AWAITINGSHIPMENTIDComponent { get; set; }
    }    
	
	public class AWAITINGSALESDOCComponent
    {
        public string type { get; set; }
        public string state { get; set; }
        public string reason { get; set; }
    }	
	public class AWAITINGSHIPMENTIDComponent
    {
        public string type { get; set; }
        public string state { get; set; }
        public string reason { get; set; }
    }
}
