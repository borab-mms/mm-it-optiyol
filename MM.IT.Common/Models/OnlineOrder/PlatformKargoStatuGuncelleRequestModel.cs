using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.OnlineOrder
{
    public class PlatformKargoStatuGuncelleRequestModel
    {
        public string Platform { get; set; }
        public string SiparisKodu { get; set; }
        public string TicariSistemKayitNo { get; set; }
        public string TakipNumarasi { get; set; }
        public string KargoDurumu { get; set; }
        public string KargoDurumuTarihi { get; set; }
    }
}
