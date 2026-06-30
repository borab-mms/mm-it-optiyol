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
/// VatKeys Tablosu 
/// </summary>
[Table("VatKeys", Schema = "MarketPlace")]
public class VatKeyEntity : IEntity
{
    public string? VatKey { get; set; }
    public decimal? MpVatRate { get; set; }
    public decimal? GlobalVatRate { get; set; }
    public DateTime CreatedDate { get; set; }

}
