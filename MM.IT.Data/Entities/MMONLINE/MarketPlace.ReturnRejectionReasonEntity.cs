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
[Table("ReturnRejectionReasons", Schema = "MarketPlace")]
public class ReturnRejectionReasonEntity : BaseEntity<int>
{
    public int? SaleChannelId { get; set; }
    public int? MarketPlaceId { get; set; }
    public string ReturnDescription { get; set; }
    public bool IsActive { get; set; }
    public int? UserCode { get; set; }
    public DateTime? UpdatedDate { get; set; }

}