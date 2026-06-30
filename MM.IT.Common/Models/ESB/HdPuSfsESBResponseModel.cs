using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.ESB
{
    public class HdPuSfsESBResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string provider { get; set; }
        public string version { get; set; }
    }
    public class DoUpsertSalesOrderResponse
    {
        public int status { get; set; }
    }
    public class ESBServiceHeader
    {
        public string messageId { get; set; }
        public string trackingId { get; set; }
        public DateTime createDate { get; set; }
        public string version { get; set; }
    }
    public class data
    {
        public ESBServiceHeader ESBServiceHeader { get; set; }
        public DoUpsertSalesOrderResponse doUpsertSalesOrderResponse { get; set; }
    }

}
