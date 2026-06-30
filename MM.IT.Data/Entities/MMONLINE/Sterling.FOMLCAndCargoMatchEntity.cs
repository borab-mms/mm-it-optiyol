using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// LCAndCargoMatch Tablosu 
/// </summary>
[Table("LCAndCargoMatch", Schema = "Sterling")]
public class FOMLCAndCargoMatchEntity : BaseEntity<int>
{

    /// <summary>
    /// LogisticCalss Bilgisi
    /// </summary>
    /// 
    public int? LogisticCalss { get; set; }

    /// <summary>
    /// flowType Bilgisi
    /// </summary>
    public string? flowType { get; set; }

    /// <summary>
    /// CargoId Bilgisi
    /// </summary>
    public string? CargoId { get; set; }

}