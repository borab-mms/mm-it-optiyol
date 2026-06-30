using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class OrderExcelModel
    {
        public string orderNumber { get; set; }
        public string orderType { get; set; }
        public string outletId { get; set; }
        public string shippingCostCalculationType { get; set; }
        public string contractPartnerId { get; set; }
        public string taxId { get; set; }
        public bool emailFlag { get; set; }
        public string artikelId { get; set; }
        public string artikelName { get; set; }
        public string brand { get; set; }
        public string vatRateProduct { get; set; }
        public string vatKey { get; set; }
        public string itemDescription { get; set; }
        public string ean { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string mobileNumber { get; set; }
    }
}
