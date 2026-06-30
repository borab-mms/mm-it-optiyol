using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Integration.VCR
{
    public class VCRInvoiceResponseModel
    {
        public bool success { get; set; }
        public object message { get; set; }
        public string version { get; set; }
        public object data { get; set; }
        public List<Error> errors { get; set; }
    }
    public class Error
    {
        public int code { get; set; }
        public string error { get; set; }
    }
}
