using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants;

/// <summary>
/// Cors için Kullanılacak Key'ler Sabit Nesnesi
/// </summary>
public static class CorsConstants
{
    /// <summary>
    /// API
    /// </summary>
    public const string API = "MM.IT.API";

    /// <summary>
    /// WEB
    /// </summary>
    public const string WEB = "MM.IT.WEB";

    /// <summary>
    /// Tümüne İzin Veren Cors Ayarlaması - Sadece Dev ve Test'te kullanılması Önerilir.
    /// </summary>
    public const string AllowAll = "MM.IT.AllowAll";

    /// <summary>
    /// Sadece MediaMarkt Sistemlerine İzin Veren Cors Ayarlaması 
    /// </summary>
    public const string OnlyMM = "MM.IT.OnlyMM";
}