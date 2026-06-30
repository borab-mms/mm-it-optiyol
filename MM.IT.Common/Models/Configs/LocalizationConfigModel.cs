using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// Default Localization Bilgisi: Uygulamanın default Culture bilgisini tutar.
/// Bilgilerin değişikliği için app.config içinde değişiklik yapılabilir.
/// </summary>
public class LocalizationConfigModel
{
    /// <summary>
    /// Uygulamanın desteklediği culture liste bilgisi
    /// </summary>
    public IEnumerable<string> SupportedCultures { get; set; }

    /// <summary>
    /// Uygulamanın default culture bilgisi
    /// </summary>
    public string DefaultCulture { get; set; }

}