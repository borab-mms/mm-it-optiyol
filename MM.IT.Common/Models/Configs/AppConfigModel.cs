using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;
/// <summary>
/// App Config Nesnesi -> app.config: AppConfig eşleniği
/// Genel config ayarları bu nesnede tanımlanır. 
/// Bilgilerin değişikliği için app.config içinde değişiklik yapılabilir.
/// </summary>
public class AppConfigModel
{
    /// <summary>
    /// Product Id Bilgisi
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// API Instance Bilgisi
    /// </summary>
    public bool IsAPI { get; set; }

    /// <summary>
    /// ClearCache metodunu aktif/pasif duruma getirir.
    /// </summary>
    public bool IsCacheCleanerActive { get; set; }

    /// <summary>
    /// ConfigureActions metodunu aktif/pasif duruma getirir.
    /// </summary>
    public bool IsActionGeneratorActive { get; set; }

    /// <summary>
    /// SeedData metodunu aktif/pasif duruma getirir.
    /// </summary>
    public bool IsSeedDataActive { get; set; }

    /// <summary>
    /// Remote Debugging Yapılırken True Olarak Set Edilip Host Edilmeli.        
    /// </summary>
    public bool IsRemoteDebuggingEnable { get; set; }

    /// <summary>
    /// Default Provider bilgisi : Api endpointleri bu bilgiyi dışarıya servis eder.
    /// </summary>
    public ProviderConfigModel Provider { get; set; }

    /// <summary>
    /// Default Localization bilgisi
    /// </summary>
    public LocalizationConfigModel Localization { get; set; }

    /// <summary>
    /// Base Url Bilgisi
    /// </summary>
    public AppConfigBaseUrlModel BaseUrl { get; set; }

    /// <summary>
    /// Base Path Bilgisi
    /// </summary>
    public string BasePath { get; set; } = "";
}

/// <summary>
/// Uygulama Base Url Bilgileri
/// </summary>
public class AppConfigBaseUrlModel
{
    /// <summary>
    /// Debug Moddaki Url Bilgisi
    /// </summary>
    public string Debug { get; set; }

    /// <summary>
    /// Release Moddaki Url Bilgisi
    /// </summary>
    public string Release { get; set; }
}
