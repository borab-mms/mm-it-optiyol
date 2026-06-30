using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Integration.VCR;

/// <summary>
/// VCR Base Integration Model Nesneleri
/// </summary>
public class VCRResponseBaseModel
{
    /// <summary>
    /// Başarılı Sonuç Bilgisi
    /// </summary>
    [JsonProperty("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Sonuç Mesajı
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Sonuç Kodu
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }
}

public class VCRAuthResponseModel : VCRResponseBaseModel
{
    /// <summary>
    /// Auth Token Bilgileri
    /// </summary>
    [JsonProperty("data")]
    public VCRAuthResponseDataModel Data { get; set; }
}

public class VCRAuthResponseDataModel
{
    /// <summary>
    /// Auth Token
    /// </summary>
    [JsonProperty("token")]
    public string Token { get; set; }

    /// <summary>
    /// Auth Son Geçerlilik Zamanı
    /// </summary>
    [JsonProperty("expiration")]
    public string Expiration { get; set; }
}