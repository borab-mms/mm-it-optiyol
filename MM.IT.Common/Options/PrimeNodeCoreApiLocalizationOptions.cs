using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Options
{
    public class PrimeNodeCoreApiLocalizationOptions
    {
        public IEnumerable<string> SupportedCultures { get; set; }

        public string DefaultCulture { get; set; }
    }
}
