using MM.IT.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;

/// <summary>
/// Hesap Entity Model Nesnesi
/// </summary>
[Serializable]
public class TenantModel : BaseEntityModel<int>
{
    /// <summary>
    /// Tanımlayıcı Bilgisi
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Aktif Mi Bilgisi
    /// </summary>
    public bool IsActive { get; set; }
}
