using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class EkolResponseModel
    {
        public string code { get; set; }
        public string keyValue { get; set; }
        public List<descriptionHead> description { get; set; }
    }
    public class descriptionHead
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }

    }
}
