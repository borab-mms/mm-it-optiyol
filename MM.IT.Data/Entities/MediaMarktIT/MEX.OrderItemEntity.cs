using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MediaMarktIT;

/// <summary>
/// OrderItems Tablosu 
/// </summary>
[Table("OrderItems", Schema = "MEX")]
public class MEXOrderItemEntity : BaseEntity<int>
{
    public string? OrderId { get; set; }
    public string? Name { get; set; }
    public string? Category1 { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? ItemIdGivenByMerchant { get; set; }
}