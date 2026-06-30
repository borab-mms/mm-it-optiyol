using MM.IT.Common.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Identity;


/// <summary>
/// Kullanıcı Modeli
/// </summary>
public class UserModel : BaseEntityModel<Guid>
{
    [JsonProperty("sub")]
    public string Code { get; set; }

    /// <summary>
    /// Kullanıcı Adı Soyadı
    /// </summary>
    [JsonProperty("name")]
    public string FullName { get; set; }

    /// <summary>
    /// Kullanıcı Adı Bilgisi
    /// </summary>
    [JsonProperty("preferred_username")]
    public string Username { get; set; }

    /// <summary>
    /// Kullanıcı Email Adresi
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// Kullanıcı Telefon Numarası
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Kullanıcının Dahil Olduğu Tenant Bilgisi
    /// </summary>
    [JsonIgnore]
    public string Tenant { get; set; }

    /// <summary>
    /// Kullanıcı Yetkileri
    /// </summary>
    public UserAuthorizationModel Authorization { get; set; }
}

/// <summary>
/// User Authorization Bilgisi İçeren Nesne
/// Aktif Account İçindeki Yetkilerini İçerir!
/// </summary>
[Serializable]
public class UserAuthorizationModel : BaseEntityModel<Guid>
{
    /// <summary>
    /// Sicil Numarası
    /// </summary>
    public int? RegistrationNumber { get; set; }

    /// <summary>
    /// Yetkili Olunan Role Listesi
    /// </summary>
    public string[] Roles { get; set; }

    /// <summary>
    /// Yetkili Olunan Action Id Listesi
    /// </summary>
    public int[] Actions { get; set; }
}


/// <summary>
/// Kullanıcı Modeli
/// </summary>
public class UserViewModel
{
    /// <summary>
    /// Kullanıcı Adı Soyadı
    /// </summary>
    public string FullName { get; set; }
}

