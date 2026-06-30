using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.OnlineOrder
{
    public class OrderShipmentStatusSummaryModel
    {
        public string CustomerOrderNumber { get; set; }
        public string ShippingName { get; set; }
        public string ShippingURL { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ShippingLastStatusUpdatedDate { get; set; }
        public bool ShippingStatus { get; set; }
    }
}
