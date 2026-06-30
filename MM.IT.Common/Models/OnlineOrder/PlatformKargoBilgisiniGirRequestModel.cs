using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.OnlineOrder
{
    public class PlatformKargoBilgisiniGirRequestModel
    {
        public string Platform { get; set; }
        public string KargoSirketi { get; set; }
        public string SiparisKodu { get; set; }
        public string TakipNumarasi { get; set; }
        public string KargoTakipUrl { get; set; }
        public string TicariSistemKayitNumarasi { get; set; }
    }
}
