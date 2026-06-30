using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms
{
    public class OTPSMSSenderResponse
    {
        public int ErrorCode { get; set; }
        public int ID { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
