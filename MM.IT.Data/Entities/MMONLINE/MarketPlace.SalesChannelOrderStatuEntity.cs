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
    /// SalesChannelOrderStatus Tablosu 
    /// </summary>
    [Table("SalesChannelOrderStatus", Schema = "MarketPlace")]
    public class SalesChannelOrderStatuEntity : BaseEntity<int>
    {
        public int? SalesChannelId { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }

    }
}
