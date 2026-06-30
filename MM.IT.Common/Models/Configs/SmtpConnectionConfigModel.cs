using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// appsettings.json SmtpConnections Config Model Nesnesi
/// </summary>
public class SmtpConnectionConfigModel
{
    /// <summary>
    /// Default Smtp Server Connection Bilgisi
    /// </summary>
    public SmtpConnectionModel DefaultConnection { get; set; }
}

/// <summary>
/// Smtp Server Connection Model
/// </summary>
public class SmtpConnectionModel
{
    /// <summary>
    /// Host Bilgisi
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Port Bilgisi
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// SSL Kullanılacak Mı Bilgisi
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// Template Kullanılsın Mı Bilgisi
    /// </summary>
    public bool UseTemplate { get; set; }

    /// <summary>
    /// Template Kullanılacaksa, Custom Template kullanılmadığı sürece kullanılacak template
    /// </summary>
    public string DefaultTemplate { get; set; }

    /// <summary>
    /// Authentication Aktif Mi Bilgisi
    /// </summary>
    public bool IsAuthenticationEnabled { get; set; }

    /// <summary>
    /// Kullanıcı Adı Bilgisi
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Şifre Bilgisi
    /// </summary>
    public string Password { get; set; }
}

