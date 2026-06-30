using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class UpdateOrderStatusResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public DataUpdateOrder data { get; set; }
    }
    public class DataUpdateOrder
    {
        public string OrderHeadId { get; set; }
    }
}
