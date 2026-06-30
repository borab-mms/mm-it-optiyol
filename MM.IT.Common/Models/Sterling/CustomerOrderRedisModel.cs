using MM.IT.Common.Models.Sms.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    public class CustomerOrderRedisModel
    {
        public CustomerOrder CustomerOrder { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CustomerOrderRedisModelForError
    {
        public CustomerOrderRedisModel CustomerOrderRedisModel { get; set; }
        public string Error { get; set; }
    }
}
