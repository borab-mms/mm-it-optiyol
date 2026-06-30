using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// RawCancellationDatas Tablosu 
    /// </summary>
    [Table("RawCancellationDatas", Schema = "MarketPlace")]
    public class RawCancellationDataEntity : BaseEntity<int>
    {
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }

    }
}
