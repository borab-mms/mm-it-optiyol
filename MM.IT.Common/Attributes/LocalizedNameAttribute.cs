using MM.IT.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace MM.IT.Common.Attributes;


/// <summary>
/// Enum için kullanılacak localization attribute nesnesi
/// </summary>
public class LocalizedNameAttribute : DescriptionAttribute
{
    private readonly string _resourceKey;
    private readonly ResourceManager _resource;
    private readonly CultureInfo _cultureInfo;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="resourceKey">Resource Key</param>
    public LocalizedNameAttribute(string resourceKey, string culture = null)
    {
        _resource = new ResourceManager(typeof(SharedResources));
        _resourceKey = resourceKey;
        _cultureInfo = !string.IsNullOrWhiteSpace(culture) ?
            new CultureInfo(culture) :
            CultureInfo.CurrentCulture;
    }

    /// <summary>
    /// Description Attribute override edildi. Resource bilgisi çekip geri döndürür.
    /// </summary>
    public override string Description
    {
        get
        {
            string displayName = _resource.GetString(_resourceKey, _cultureInfo);

            return string.IsNullOrEmpty(displayName)
                ? string.Format("[[{0}]]", _resourceKey)
                : displayName;
        }
    }
}