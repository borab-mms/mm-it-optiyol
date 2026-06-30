using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;

/// <summary>
/// Context Response Düzenlemesi İçin Kullanılan Input Model Nesnesi
/// </summary>
public class ContextResultModel
{
    /// <summary>
    /// Ajax Request'lerde Dönecek Http Code Bilgisi
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Ajax Request'lerde Dönecek Mesaj Bilgisi
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Ajax Dışı Request'lerde Redirect Edilecek Url Bilgisi
    /// </summary>
    public string RedirectUrl { get; set; }
}