using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class ArvatoOutboundOrderAddRequestModel
    {
        public List<ItemList> itemList { get; set; }
        public bool continueOnError { get; set; }
    }
    public class ItemList
    {
        public string accountCode { get; set; }
        public string brandcode { get; set; }
        public string serviceCode { get; set; }
        public string platformCode { get; set; }
        public string organizationCode { get; set; }
        public string orderNumber { get; set; }
        public DateTime orderTime { get; set; }
        public string orderType { get; set; }
        public string carrierCode { get; set; }
        public string customerReferenceNumber { get; set; }
        public string note { get; set; }
        public List<OrderPartyDetailList> orderPartyDetailList { get; set; }
        public List<OrderDetailList> orderDetailList { get; set; }
    }
    public class OrderDetailAttribute
    {
        public string var01String { get; set; }
        public string var02String { get; set; }
        public string var03String { get; set; }
        public string var04String { get; set; }
    }
    public class OrderDetailList
    {
        public string lineNumber { get; set; }
        public string itemCode { get; set; }
        public string packTypeCode { get; set; }
        public int quantity { get; set; }
        public OrderDetailAttribute orderDetailAttribute { get; set; }
    }
    public class OrderPartyDetailList
    {
        public string typeCode { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public string cityCode { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobilePhone { get; set; }
        public string zipCode { get; set; }
        public string addressText { get; set; }
    }
}
