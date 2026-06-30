using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class ReturnDemandResponseModel
    {
       
        public DataItem Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

    }
    public class DataItem
    {
        public string Result { get; set; }

    }
}
