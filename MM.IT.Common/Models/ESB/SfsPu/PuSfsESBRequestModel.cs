using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.ESB.SfsPu
{
    public class PuSfsESBRequestModel
    {
        public string environment { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public OrderHeaderSfsPu orderHeader { get; set; }
        public List<OrderDetail> orderDetail { get; set; }
    }
    public class OrderHeaderSfsPu
    {
        public string orderNumber { get; set; }
        public string orderType { get; set; }
        public string outletId { get; set; }
        public string shippingCostCalculationType { get; set; }
        public Totals totals { get; set; }
        public DeliveryDetailsSfsPu deliveryDetails { get; set; }
        public Customer customer { get; set; }
        public PaymentInformation paymentInformation { get; set; }
        public string contractPartnerId { get; set; }
        public string taxId { get; set; }
        public bool emailFlag { get; set; }
        public Notes notes { get; set; }
    }
    public class DeliveryDetailsSfsPu
    {
        public string deliveryType { get; set; }
        public string customerRequestedDeliveryType { get; set; }
        public string carrier { get; set; }
        public string pickupOutletId { get; set; }
        public string pickupOutletName { get; set; }
        public outletAddressItem outletAddress { get; set; }
    }    
    public class outletAddressItem
    {
        //public string addressField_1 { get; set; }
        //public string zipCode_1 { get; set; }
        //public string zipCode_2 { get; set; }
        //public string city { get; set; }
        //public string country { get; set; }
        //public string phoneNumber { get; set; }
        //public string faxNumber { get; set; }

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string zipCode1 { get; set; }
        public string zipCode2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string faxNumber { get; set; }
    }
}
