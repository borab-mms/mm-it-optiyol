using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.ESB
{
    public class SendDataToESBResponseModel
    {
        public bool Success { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
    }
}
