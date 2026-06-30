using MM.IT.Data.Entities.Base;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// ReturnDemandStatus Tablosu 
/// </summary>
[Table("ReturnDemandStatus", Schema = "MarketPlace")]
public class ReturnDemandStatuEntity : IEntity
{
    public int Id { get; set; }
    public string StatuName { get; set; }
    public bool IsActive { get; set; }
    public int? UserCode { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }

}