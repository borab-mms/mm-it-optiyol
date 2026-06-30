using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.FOM
{

    /// <summary>
    /// Fom Data DB -> EKOL_Order_Head Entity Nesnesi
    /// </summary>
    [Table("EKOL_Order_Head", Schema = "dbo")]
    public class EKOLOrderHeadEntity : IEntity
    {
        public string OrderNo { get; set; }
        public string CarrierTrackingNr { get; set; }
        public int StatusId { get; set; }
        public string CarrierLink { get; set; }
        public DateTime? ExpectedDateDelivery { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
