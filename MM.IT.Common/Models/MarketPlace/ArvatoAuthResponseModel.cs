using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class ArvatoAuthResponseModel
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public data data { get; set; }
    }
    public class data
    {
        public string token { get; set; }
    }
}
