using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{

    public class OrderCancellationRequestRedisModel
    {
        public OrderCancellationRequestModel OrderCancellationRequestModel { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
