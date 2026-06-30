using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.EKOLStock
{
    public class EKOLDesiRedisModel
    {
        public List<EKOLDesiModel> EKOLDesis { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
