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
    /// T800SendLog Tablosu 
    /// </summary>
    [Table("T800SendLog", Schema = "Sterling")]
    public class FOMT800SendLogEntity : BaseEntity<int>
    {
        /// <summary>
        /// Şipariş Numarası Bilgisi
        /// </summary>
        public string? CustomerOrderNumber { get; set; }
        /// <summary>
        /// FulfillmentOrderId Bilgisi
        /// </summary>
        public string? FulfillmentOrderId { get; set; }
        /// <summary>
        /// Request Bilgisi
        /// </summary>
        public string? Request { get; set; }
        /// <summary>
        /// Response Bilgisi
        /// </summary>
        public string? Response { get; set; }
        /// <summary>
        /// IsSuccess Bilgisi
        /// </summary>
        public bool? IsSuccess { get; set; }
    }
}
