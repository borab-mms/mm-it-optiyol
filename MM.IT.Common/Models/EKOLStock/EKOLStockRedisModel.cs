using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.EKOLStock
{
    public class EKOLStockRedisModel
    {
        public List<EKOLStockModel> EKOLStocks { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Key { get; set; }
    }
    public class EKOLStockRedisOnlyColumnModel
    {
        public DateTime CreatedDate { get; set; }
        public string Key { get; set; }
    }
}
