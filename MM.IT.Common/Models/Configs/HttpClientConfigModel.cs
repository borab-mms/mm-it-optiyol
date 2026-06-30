using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// Http Client Bağlantı Bilgileri Config Nesneleri -> appsettings.json -> HttpClientConnections
/// </summary>
public class HttpClientConfigModel
{
    /// <summary>
    /// SMS Sender API Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel MobileDevSmsConnection { get; set; }
    /// <summary>
    /// WSC API Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel WSCConnection { get; set; }
    /// <summary>
    /// EKOL Desi Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel EKOLDesiConnection { get; set; }
    /// <summary>
    /// EKOL Stock Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel EKOLStockConnection { get; set; }
    /// <summary>
    /// MMGlobalAuth Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel MMGlobalAuthConnection { get; set; }
    /// <summary>
    /// MMGlobalV3Auth Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel MMGlobalV3AuthConnection { get; set; }
    /// <summary>
    /// MMGlobal Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel MMGlobalConnection { get; set; }
    /// <summary>
    /// MMGlobalv3 Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel MMGlobalV3Connection { get; set; }

    /// <summary>
    /// ESB Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel ESBConnection { get; set; }

    /// <summary>
    /// AyenSoft Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel AyenSoftConnection { get; set; }    
    
    /// <summary>
    /// AyenSoft Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel EpayConnection { get; set; }

    /// <summary>
    /// SmsSenderConnection Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel SmsSenderConnection { get; set; }

    /// <summary>
    /// AGTConnection Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel AGTConnection { get; set; }

    /// <summary>
    /// VCRConnection Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel VCRConnection { get; set; }

    /// <summary>
    /// TMSConnection Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel TMSConnection { get; set; }

    /// <summary>
    /// ArvatoAuthConnection Servisi Bağlantı Bilgileri
    /// </summary>
    public HttpClientConnectionModel ArvatoAuthConnection { get; set; }
    public HttpClientConnectionModel OptiyolConnection { get; set; }
}

/// <summary>
/// Http Client Servisi Bağlantı Bilgileri Model Nesnesi
/// </summary>
public class HttpClientConnectionModel
{
    /// <summary>
    /// Base Url Bilgisi
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Kullanıcı Adı
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Şifre
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Parametreler 
    /// </summary>
    public Dictionary<string, string> Parameters { get; set; }
}
