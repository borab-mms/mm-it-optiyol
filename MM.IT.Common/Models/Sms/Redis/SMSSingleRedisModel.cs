
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms.Redis
{
    public class SMSSingleRedisModel
    {
        public List<SMSSingleModel> SMSSingleModels { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
