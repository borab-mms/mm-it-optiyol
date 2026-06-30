using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MediaMarkt
{
    public class StockModel
    {
        public List<StoreStockModel> StoreStocks { get; set; }
    }
    public class StoreStockModel
    {

        /// <summary>
        /// Stok Kodu Bilgisi
        /// </summary>
        public int StockCode { get; set; }
        /// <summary>
        /// Stok Adedi Bilgisi
        /// </summary>
        public int StockQuantity { get; set; }
        /// <summary>
        /// ReservationQuantity Adedi Bilgisi
        /// </summary>
        [JsonIgnore]
        public int ReservationQuantity { get; set; }
    }
}
