using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling.CosV1
{
    public class CosV1CustomerOrderModel
    {
        public List<CustomerOrderItemModel> customer_orders { get; set; }
    }

    public class BillingAddress
    {
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string title { get; set; }
        public string company { get; set; }
        public List<string> street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zip_code { get; set; }
    }

    public class BusinessPersona
    {
        public string customer_user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string tax_id { get; set; }
        public BillingAddress billing_address { get; set; }
    }

    public class CustomerOrderItemModel
    {
        public string coo_version { get; set; }
        public string source_oms { get; set; }
        public string customer_order_number { get; set; }
        public DateTime? order_date { get; set; }
        public string order_language { get; set; }
        public DateTime? last_modified { get; set; }
        public string order_type { get; set; }
        public string customerType { get; set; }
        public string orderMethod { get; set; }
        public bool isNewOrder { get; set; }
        public string dbInsertError { get; set; }
        public string order_status { get; set; }
        public bool isAGT { get; set; }
        public bool isAGTSuccess { get; set; }
        public bool? isVCR { get; set; }
        public bool isT800MP { get; set; }
        public bool isShipped { get; set; }
        public bool isSendToArvato { get; set; }
        public int? SellerId { get; set; }
        public string SellerType { get; set; }
        public string SellerName { get; set; }
        public string country_organization { get; set; }
        public string brand { get; set; }
        public string sales_channel { get; set; }
        public bool order_hold_flag { get; set; }
        public string hold_status { get; set; }
        public string hold_type { get; set; }
        public string hold_reason { get; set; }
        public double? order_value { get; set; }
        public double? order_tax_value { get; set; }
        public string order_tax_type { get; set; }
        public string order_currency { get; set; }
        public int? contractual_partner { get; set; }
        public string reservation_id { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public string fulfillment_method { get; set; }
        public BusinessPersona business_persona { get; set; }
        public loyalty loyalty { get; set; }
        public List<Payment> payments { get; set; }
        public List<OrderDiscount> order_discounts { get; set; }
        public List<OrderCharge> order_charges { get; set; }
        public List<LineItemRelation> line_item_relation { get; set; }
        public List<Item> item { get; set; }
        public List<Shipment> shipments { get; set; }
    }
    public class Shipment
    {
        public List<ShipmentLine> shipment_lines { get; set; }
        public string shipment_number { get; set; }
        public string status { get; set; }
        public string ship_node { get; set; }
        public string sales_doc_number { get; set; }
        public string fulfillment_order_id { get; set; }
        public string carrier_name { get; set; }
        public string pickup_location { get; set; }
    }

    public class ShipmentLine
    {
        public string shipment_line_no { get; set; }
        public string line_item_ref { get; set; }
        public string quantity { get; set; }
        public List<object> serial_numbers { get; set; }
    }
    public class LineItemRelation
    {
        public string line_item_id_parent { get; set; }
        public string line_item_id_child { get; set; }
        public string line_item_id_relationship_type { get; set; }
    }
    public class OrderCharge
    {
        public string order_charge_type { get; set; }
        public string order_charge_description { get; set; }
        public string order_charge_value { get; set; }
        public string order_charge_name { get; set; }
        public string order_charge_vat_sign { get; set; }
        public string? order_charge_vat_percentage { get; set; }
    }
    public class GrossAmount
    {
        public decimal? amount { get; set; }
        public string currency { get; set; }
    }

    public class OrderDiscount
    {
        public string order_discount_type { get; set; }
        public string order_discount_code { get; set; }
        public string order_discount_description { get; set; }
        public string? order_discount_value { get; set; }
        public string order_discount_reason_code { get; set; }

    }
    public class returnStatuse
    {
        public string return_status { get; set; }
        public decimal? status_quantity { get; set; }

    }
    public class returnItem
    {
        public string? return_order_id { get; set; }
        public DateTime? return_created { get; set; }
        public string return_reason_code { get; set; }
        public int? return_outlet_id { get; set; }
        public string customer_return_reason { get; set; }
        public string return_channel { get; set; }
        public int? return_quantity { get; set; }
        public int? address { get; set; }
        public List<returnStatuse> return_statuses { get; set; }

    }
    public class loyalty
    {
        public string loyalty_id { get; set; }
        public double? loyalty_win { get; set; }
        public string loyalty_win_type { get; set; }

    }
    public class WarrantyItem
    {
        public string warranty_certificate_number { get; set; }
        public string warranty_item_serial_number { get; set; }
        public string warranty_contract_period { get; set; }
        public string warranty_number { get; set; }

    }

    public class Item
    {
        public string id { get; set; }
        public List<returnItem> returns { get; set; }
        public string line_item_id { get; set; }
        public string line_item_reference { get; set; }
        public string line_item_status_description { get; set; }
        public string hold_status { get; set; }
        public string hold_type { get; set; }
        public string hold_reason { get; set; }
        public string product_serial_number { get; set; }
        public List<object> serial_numbers { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public int? pgId { get; set; }
        public string pgName { get; set; }
        public string manufacturer { get; set; }
        public double? product_price { get; set; }
        public double? original_product_price { get; set; }
        public double? total_quantity { get; set; }
        public double? item_price { get; set; }
        public RetailItemPrice retail_item_price { get; set; }
        public RetailUnitPrice retail_unit_price { get; set; }
        public string logistics_class { get; set; }
        public int? vat_sign { get; set; }
        public string? vat_percentage { get; set; }
        public List<WarrantyItem> warranties { get; set; }
        public Quantities quantities { get; set; }
        public List<ItemDiscount> item_discounts { get; set; }
        public object charges { get; set; }
        public List<Fulfillment> fulfillment { get; set; }
        public int? position { get; set; }
        public string cargoCode { get; set; }
        public string cargoCompany { get; set; }
        public DateTime? expected_delivery_date { get; set; }
    }

    public class ItemDiscount
    {
        public string item_discount_type { get; set; }
        public string item_discount_code { get; set; }
        public string item_discount_description { get; set; }
        public string item_discount_value { get; set; }
        public string item_discount_unit_value { get; set; }
        public string item_discount_reason_code { get; set; }
        public string item_discount_name { get; set; }
        public string item_discount_reference { get; set; }
        public string item_discount_cost_center { get; set; }
        public string item_discount_class { get; set; }
        public List<ChargeItem> charges { get; set; }
    }
    public class ChargeItem
    {
        public string item_charge_type { get; set; }
        public string item_charge_description { get; set; }
        public string item_charge_value { get; set; }
    }
    public class NetAmount
    {
        public decimal? amount { get; set; }
        public string currency { get; set; }
    }

    public class Payment
    {
        public string payment_id { get; set; }
        public string payment_ref { get; set; }
        public string payment_type { get; set; }
        public string payment_status { get; set; }
        public string payment_value { get; set; }
        public string payment_currency { get; set; }
    }

    public class Quantities
    {
        public double? quantity_ordered { get; set; }
        public double? quantity_shipped { get; set; }
        public double? quantity_returned { get; set; }
        public double? quantity_cancelled { get; set; }
        public object quantity_replaced { get; set; }
    }

    public class RetailItemPrice
    {
        public GrossAmount gross_amount { get; set; }
        public NetAmount net_amount { get; set; }
        public VatAmount vat_amount { get; set; }
        public RetailUnitPrice retail_unit_price { get; set; }
    }

    public class RetailUnitPrice
    {
        public GrossAmount gross_amount { get; set; }
        public NetAmount net_amount { get; set; }
        public VatAmount vat_amount { get; set; }
    }

    public class ShippingAddress
    {
        public string address_type { get; set; }
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company_name { get; set; }
        public string department { get; set; }
        public string phone_number { get; set; }
        public string mobile_number { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string zip_code { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string address_id { get; set; }
        public string pickup_location { get; set; }
    }

    public class VatAmount
    {
        public decimal? amount { get; set; }
        public string currency { get; set; }
    }
    public class Address
    {
        public string address_type { get; set; }
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company_name { get; set; }
        public string department { get; set; }
        public string phone_number { get; set; }
        public string mobile_number { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string zip_code { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string address_id { get; set; }
    }

    public class Delivery
    {
        public string shipment_number { get; set; }
        public string delivery_date_promise { get; set; }
        public string delivery_date_actual { get; set; }
        public string wdd_from_delivery_time { get; set; }
        public string wdd_to_delivery_time { get; set; }
        public string carrier_name { get; set; }
        public string quantity_shipped { get; set; }
        public List<Package> packages { get; set; }
    }

    public class Fulfillment
    {
        public string fulfillment_order_id { get; set; }
        public string fulfillment_method { get; set; }
        public string fulfillment_outlet_procure_node_id { get; set; }
        public string fulfillment_outlet_ship_node_id { get; set; }
        public string fulfillment_quantity { get; set; }
        public string pickup_date { get; set; }
        public List<Address> address { get; set; }
        public List<Delivery> deliveries { get; set; }
    }

    public class Package
    {
        public string package_number { get; set; }
        public string tracking_url { get; set; }
        public string tracking_id { get; set; }
    }

}
