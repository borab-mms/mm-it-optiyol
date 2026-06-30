using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// Default Provider bilgisi : Api endpointleri ve Swagger bu bilgiyi dışarıya servis eder.
/// Bilgilerin değişikliği için app.config içinde değişiklik yapılabilir.
/// </summary>
public class ProviderConfigModel
{
    /// <summary>
    /// Provider Adı : Api Endpoint ve Swagger bu bilgiyi dışarıya servis eder.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Provider İletişim Adı: Swagger bu bilgiyi dışarıya servis eder.
    /// </summary>
    public string ContactName { get; set; }
    /// <summary>
    /// Provider İletişim Email Adresi: Swagger bu bilgiyi dışarıya servis eder.
    /// </summary>
    public string ContactEmail { get; set; }
    /// <summary>
    /// Provider Detay Bilgisi: Swagger bu bilgiyi dışarıya servis eder.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Provider Version :  Api Endpoint ve Swagger bu bilgiyi dışarıya servis eder.
    /// </summary>
    public string Version { get; set; }
}
