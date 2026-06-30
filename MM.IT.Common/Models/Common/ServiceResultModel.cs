using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;

/// <summary>
/// Business Service Result Model.
/// </summary>
/// <typeparam name="TDataModel">TDataModel: Data Tipi</typeparam>
[Serializable]
public class ServiceResultModel<TDataModel>
{
    /// <summary>
    /// Status Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public TDataModel Data { get; set; }

    /// <summary>
    /// Sağlayıcı Bilgisi: appsettings.json içinde bulunabilir.
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// Uygulama Versiyon Bilgisi
    /// </summary>
    public string Version { get; set; }
}

/// <summary>
/// Object tipli data barındıran Service Result Model Nesnesi
/// </summary>
[Serializable]
public class ServiceResultModel : ServiceResultModel<object>
{

}