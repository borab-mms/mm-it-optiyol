using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MediaMarkt
{
    public class StockRequestModel
    {
        public List<StockRequestItemModel> ArtikelIds { get; set; }
    }
    public class StockRequestItemModel
    {
        public int ArtikelId { get; set; }
    }
}
