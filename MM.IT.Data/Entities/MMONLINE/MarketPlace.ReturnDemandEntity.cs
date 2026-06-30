using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// ReturnDemands Tablosu 
/// </summary>
[Table("ReturnDemands", Schema = "MarketPlace")]
public class ReturnDemandEntity : BaseEntity<int>
{
    public int Article { get; set; }
    public string ChannelCode { get; set; }
    public string OrderHeadId { get; set; }
    public decimal? Amount { get; set; }
    public string ReturnReason { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string ReturnId { get; set; }
    public string ReturnDetailId { get; set; }
    public string ShippingCompany { get; set; }
    public string CargoCode { get; set; }
    public int? ReturnDemandStatuId { get; set; }
    public int? UserCode { get; set; }
    public DateTime? UpdatedDate { get; set; }
   
}
