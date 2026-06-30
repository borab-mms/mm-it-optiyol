using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{
    public class AyenSoftResponseModel
    {
        public int StokKodu { get; set; }
        public decimal Fiyat { get; set; }
        public decimal PsfFiyat { get; set; }
        public int Stok { get; set; }
        public int MusteriTedarikciId { get; set; }
        public bool Success { get; set; }
    }
}
