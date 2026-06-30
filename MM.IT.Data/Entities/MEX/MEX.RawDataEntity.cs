using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// RawDatas Tablosu 
/// </summary>
[Table("RawDatas", Schema = "MEX")]
public class MEXRawDataEntity : BaseEntity<int>
{
    /// <summary>
    /// Şipariş Numarası Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// ResultCode Bilgisi
    /// </summary>
    /// 
    public string ResultCode { get; set; }

    /// <summary>
    /// RawData Json Bilgisi
    /// </summary>
    public string RawData { get; set; }
}

