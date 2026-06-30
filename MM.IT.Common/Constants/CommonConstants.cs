using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants;
public static class CommonConstants
{
    /// <summary>
    /// Account Sabiti. HttpContext.Items için kullanıldı.
    /// </summary>
    public const string TenantKey = "MM.IT.Tenant";

    /// <summary>
    /// Tenant Sabiti. Api Header Configration için kullanıldı.
    /// </summary>
    public const string AcceptTenantIdentifierHeaderKey = "Accept-Tenant-Identifier";

    /// <summary>
    /// Kullanıcı Sabiti. HttpContext.Items için kullanıldı.
    /// </summary>
    public const string UserKey = "MM.IT.User";

    /// <summary>
    /// Dil Sabiti. Api Header Configration için kullanıldı.
    /// </summary>
    public const string AcceptLanguageHeaderKey = "Accept-Language";

    /// <summary>
    /// Dil Sabiti. HttpContext.Items için kullanıldı. 
    /// </summary>
    public const string LanguageKey = "MM.IT.Language";
}