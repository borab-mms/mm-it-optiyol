using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Data.Entities.MMONLINE.Interfaces;

namespace MM.IT.Data.Entities.MMONLINE
{
    /// <summary>
    /// CancellationRequests Tablosu 
    /// </summary>
    [Table("CancellationRequests", Schema = "CancellationProcess")]
    public class CancellationProcessCancellationRequestEntity : ISterlingEntity<int>
    {
        /// <summary>
        /// CustomerOrderNumber Bilgisi
        /// </summary>
        public int? CustomerOrderNumber { get; set; }

        /// <summary>
        /// FulfillmentOrderId Bilgisi
        /// </summary>
        public string? FulfillmentOrderId { get; set; }

        /// <summary>
        /// OutletId Bilgisi
        /// </summary>
        public int? OutletId { get; set; }

        /// <summary>
        /// ReasonId Bilgisi
        /// </summary>
        public int? ReasonId { get; set; }

        /// <summary>
        /// OtherDescription Bilgisi
        /// </summary>
        public string? OtherDescription { get; set; }

        /// <summary>
        /// Status Bilgisi
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// UpdatedDate Bilgisi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
