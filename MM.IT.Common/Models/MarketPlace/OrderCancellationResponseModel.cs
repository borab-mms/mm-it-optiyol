using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class OrderCancellationResponseModel
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public int code { get; set; }
        public DataUpdateOrder data { get; set; }
    }
}
