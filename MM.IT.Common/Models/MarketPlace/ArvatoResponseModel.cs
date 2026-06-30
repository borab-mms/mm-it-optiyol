using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class ArvatoResponseModel
    {
        public string requestID { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public List<dataHead> data { get; set; }
        public bool isAsync { get; set; }
    }
    public class dataHead
    {
        public string orderNumber { get; set; }
        public bool isValid { get; set; }
        public bool isInformationMessage { get; set; }
        public List<responseMessagesHead> responseMessages { get; set; }
    }
    public class responseMessagesHead
    {
        public int code { get; set; }
        public string description { get; set; }
    }
}
