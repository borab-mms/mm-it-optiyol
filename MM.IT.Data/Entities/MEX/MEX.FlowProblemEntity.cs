using MM.IT.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MEX;

/// <summary>
/// FlowProblems Tablosu 
/// </summary>
[Table("FlowProblems", Schema = "MEX")]
public class MEXFlowProblemEntity : BaseEntity<int>
{
    /// <summary>
    /// Şipariş Numarası Bilgisi
    /// </summary>
    public string CustomerOrderNumber { get; set; }

    /// <summary>
    /// Source Bilgisi
    /// </summary>
    /// 
    public string Source { get; set; }

    /// <summary>
    /// UserInfo Bilgisi
    /// </summary>
    public string UserInfo { get; set; }

    /// <summary>
    /// UpdatedDate Bilgisi
    /// </summary>
    public string UpdatedDate { get; set; }
}

