using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// OI_DeliveryMethods Tablosu 
/// </summary>
[Table("OI_DeliveryMethods", Schema = "dbo")]
public class OIDeliveryMethodEntity : BaseEntityUpperCaseIDEntity<int>
{

    public string? FulfillmentMethod { get; set; }
    public string? FulfillmentMethodMap { get; set; }
    public string? OrderMethod { get; set; }
    public bool IsActive { get; set; }
}
