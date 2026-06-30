using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    public class Data
    {
        public List<ResultGoodsOut> resultGoodsOut { get; set; }
    }

    public class ResultGoodsOut
    {
        public string orderNo { get; set; }
        public string keyValue { get; set; }
        public string headerResult { get; set; }
        public string result { get; set; }
    }

    public class CreateInvoiceResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public string provider { get; set; }
        public string version { get; set; }
    }


}
