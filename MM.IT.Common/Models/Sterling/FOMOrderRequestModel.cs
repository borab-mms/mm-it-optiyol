using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{

    public class FOMOrderRequestModel
    {
        public string customerOrderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public int contractualPartner { get; set; }
        public string saFirstName { get; set; }
        public string saLastName { get; set; }
        public string saMobileNumber { get; set; }
        public string saAddressLine1 { get; set; }
        public string saAddressLine2 { get; set; }
        public string saZipCode { get; set; }
        public string saCity { get; set; }
        public string bpCustomerUserId { get; set; }
        public string bpBaFirstName { get; set; }
        public string bpBaLastName { get; set; }
        public List<FOMOrderRequestItemModel> items { get; set; }
    }
    public class FOMOrderRequestItemModel
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int totalQuantity { get; set; }
        public string fulfillmentOrderId { get; set; }
    }

}
