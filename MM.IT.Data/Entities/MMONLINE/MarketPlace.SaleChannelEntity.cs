using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// ReturnRejectionReasons Tablosu 
/// </summary>
[Table("SalesChannels", Schema = "MarketPlace")]
public class SaleChannelEntity : BaseEntity<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Prefix { get; set; }
    public int StartValue { get; set; }
    public int? SellerId { get; set; }
    public bool IsShippedToIntegrator { get; set; }
    public bool IsActive { get; set; }
    public int UserCode { get; set; }
    public DateTime? UpdatedDate { get; set; }

}