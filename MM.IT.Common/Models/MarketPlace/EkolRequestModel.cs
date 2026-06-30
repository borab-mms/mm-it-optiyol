using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class EkolRequestModel
    {
        public HEADERITEM HEADER { get; set; }
        public DATAITEM DATA { get; set; }
    }
    public class HEADERITEM
    {
        public string GUID { get; set; }
        public string TRAN_NR { get; set; }
    }
    public class DATAITEM
    {
        public string WORK_ORDER_NO { get; set; }
    }
}
