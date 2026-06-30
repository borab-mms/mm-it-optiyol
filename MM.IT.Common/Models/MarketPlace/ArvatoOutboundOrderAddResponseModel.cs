using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class ArvatoOutboundOrderAddResponseModel
    {
        public string requestID { get; set; }
        public bool isSuccess { get; set; }
        public List<object> exceptionMessage { get; set; }
        public int statusCode { get; set; }
        public List<Datum> data { get; set; }
        public bool isAsync { get; set; }
        public double duration { get; set; }
    }
    public class Datum
    {
        public string orderNumber { get; set; }
        public bool isValid { get; set; }
        public bool isInformationMessage { get; set; }
        public List<ResponseMessage> responseMessages { get; set; }
    }
    public class ResponseMessage
    {
        public int code { get; set; }
        public string description { get; set; }
    }
}
