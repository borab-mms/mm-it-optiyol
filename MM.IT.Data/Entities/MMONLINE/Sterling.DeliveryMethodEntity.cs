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
    /// DeliveryMethods Tablosu 
    /// </summary>
    [Table("DeliveryMethods", Schema = "Sterling")]
    public class FOMDeliveryMethodEntity : BaseEntity<int>
    {
        public string? FulfillmentMethod { get; set; }
        public string? FulfillmentMethodMap { get; set; }
        public string? OrderMethod { get; set; }
        public bool IsActive { get; set; }
    }
}
