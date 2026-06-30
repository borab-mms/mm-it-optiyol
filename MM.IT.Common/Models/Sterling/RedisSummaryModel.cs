using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    public class RedisSummaryModel
    {
        public string ChannelName { get; set; } 
        public CustomerOrderRedisModel CustomerOrderRedisModel { get; set; }
    }
}
