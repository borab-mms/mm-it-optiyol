using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMONLINE;

/// <summary>
/// RawDatas Tablosu 
/// </summary>
[Table("RawDatas", Schema = "Sterling")]
public class FOMRawDataEntity : BaseEntity<int>
{
    /// <summary>
    /// Şipariş Numarası Bilgisi
    /// </summary>
    public string? CustomerOrderNumber{ get; set; }

    /// <summary>
    /// ResultCode Bilgisi
    /// </summary>
    /// 
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// StartProcessingDate Bilgisi
    /// </summary>
    /// 
    public DateTime? StartProcessingDate { get; set; }

    /// <summary>
    /// RawData Json Bilgisi
    /// </summary>
    public string? RawData { get; set; }
}
