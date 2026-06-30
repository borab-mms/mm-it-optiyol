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
    /// SenderMpShippingStatusLog Tablosu 
    /// </summary>
    [Table("SenderMpShippingStatusLog", Schema = "Sterling")]
    public class FOMSenderMpShippingStatusLogEntity : BaseEntity<int>
    {

        /// <summary>
        /// Şipariş Numarası Bilgisi
        /// </summary>
        public string? CustomerOrderNumber { get; set; }

        /// <summary>
        /// ServiceName Bilgisi
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// ProductId Bilgisi
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Request Bilgisi
        /// </summary>
        public string? Request { get; set; }

        /// <summary>
        /// Response Bilgisi
        /// </summary>
        public string? Response { get; set; }
    }
}
