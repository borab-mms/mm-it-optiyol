using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.PIM
{
    public class JobSetting
    {
        public List<Setting> Setting { get; set; }

    }
    public class Setting
    {
        public string Key { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public bool Enabled { get; set; }
        public string Cron { get; set; }
    }
}
