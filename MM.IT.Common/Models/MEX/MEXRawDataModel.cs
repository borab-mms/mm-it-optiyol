using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MEX
{
    public class MEXRawDataModel
    {
        public string Key { get; set; }
        public PaymentResponse PaymentResponse { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string ResultCode { get; set; }
        public string RawData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
