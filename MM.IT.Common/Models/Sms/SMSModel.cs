using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using MM.IT.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Attributes;

namespace MM.IT.Common.Models.Sms;

#region Authorization Models

public class SMSAuthResponseModel : SMSResponseBaseModel
{
    /// <summary>
    /// Auth Token Bilgileri
    /// </summary>
    public SMSAuthResponseDataModel data { get; set; }
}

public class SMSAuthResponseDataModel
{
    /// <summary>
    /// Auth Token
    /// </summary>
    public string token { get; set; }

    /// <summary>
    /// Auth Son Geçerlilik Zamanı
    /// </summary>
    public string expiration { get; set; }
}

#endregion

#region SMS Models

public class SMSResponseBaseModel
{
    /// <summary>
    /// Başarılı Sonuç Bilgisi
    /// </summary>
    public bool success { get; set; }

    /// <summary>
    /// Sonuç Mesajı
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// Sonuç Kodu
    /// </summary>
    public int code { get; set; }
}

public class SMSResponseModel : SMSResponseBaseModel
{
    /// <summary>
    /// Başarılı Sonuç Bilgisi SMS’in ulaştığı anlamına gelmez.
    /// Sadece SMS talebinin gönderim için alındığı ve MobilDev’e ilettiği anlamına gelir.
    /// SMS’lerin müşteriye ulaşıp ulaşılmadığı bilgisi için: "data" altında bulunan "id" ile işlem numarası dönüyor.
    /// </summary>
    public SMSResponseDataModel data { get; set; }
}

public class SMSResponseDataModel
{
    /// <summary>
    /// Hata Kodu
    /// </summary>
    public int errorCode { get; set; }

    /// <summary>
    /// SMS'in ulaşıp ulaşmadığı bilgisi
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Durum
    /// </summary>
    public bool status { get; set; }
}

#region Single Request

public class SmsSingleRequestModel : SmsBaseRequestModel
{
    /// <summary>
    /// Mesaj İçeriği / Zorunlu - maks 400 karakter
    /// </summary>
    public string mesgbody { get; set; }
    /// <summary>
    /// SMSContentId İçeriği / Zorunlu değil
    /// </summary>
    public int SMSContentId { get; set; }

    /// <summary>
    /// GSM Numaraları / comma seperated / Zorunlu - maks 300 numara
    /// </summary>
    public string numbers { get; set; }
}

/// <summary>
/// SmsSingleRequestModel Fluent Validation Modeli
/// </summary>
public class SmsSingleRequestModelValidator : AbstractValidator<SmsSingleRequestModel>, IPmModelValidator
{
    /// <summary>
    /// Constructor
    /// </summary>
    public SmsSingleRequestModelValidator()
    {
        RuleFor(p => p.numbers)
            .NotNull()
            .NotEmpty()
            .Must(p =>
            {
                var count = p.Split(",")
                .Where(q => !string.IsNullOrWhiteSpace(q) && q.ToLong().HasValue)
                .Count();

                return count > 0 && count <= 3000;
            });

        RuleFor(p => p.mesgbody)
              .NotNull()
              .NotEmpty()
              .Must(p =>
              {
                  var counter = 0;
                  var turkishCharacter = new char[] { 'Ş', 'ş', 'ç', 'Ğ', 'ğ', 'İ', 'ı' };

                  for (var index = 0; index < p.Length; index++)
                  {
                      var item = p[index];

                      if (turkishCharacter.Contains(item))
                      {
                          counter++;
                      }

                      counter++;
                  }

                  return counter <= 894;
              });

        RuleFor(p => p.messageDescription)
           .NotNull()
           .NotEmpty()
           .Length(1, 50);

        RuleFor(p => p.info1)
           .Length(0, 150);

        RuleFor(p => p.info2)
           .Length(0, 150);

        RuleFor(p => p.info3)
           .Length(0, 150);
    }
}

#endregion



public class SmsMultipleRequestModel : SmsBaseRequestModel
{
    /// <summary>
    /// GSM ve Mesaj Listesi
    /// </summary>
    public List<SmsMultipleMessageModel> messages { get; set; }
}

public class SmsBaseRequestModel
{
    /// <summary>
    /// Raporlama için kısa açıklama / Zorunlu - maks 50 karakter
    /// </summary>
    public string messageDescription { get; set; }

    /// <summary>
    /// Platform / Zorunlu - sabit değer
    /// </summary>
    public string channelCode { get; set; } = "Platform";

    /// <summary>
    /// Servisin çağırıldığı anın bilgisi / Zorunlu - 2022-11-27T12:09:37.325Z
    /// </summary>
    public string sDate { get; set; } = DateTime.UtcNow.ToString("O");

    /// <summary>
    /// Servisin çağırıldığı andan 1 saat sonrası / Zorunlu - 2022-11-27T13:09:37.325Z
    /// </summary>
    public string eDate { get; set; } = DateTime.UtcNow.AddHours(1).ToString("O");

    /// <summary>
    /// N / Zorunlu - sabit değer
    /// </summary>
    public string messageType { get; set; } = "N";

    /// <summary>
    /// Zorunlu - boş
    /// </summary>
    public string recipientType { get; set; } = "";

    /// <summary>
    /// Raporlama için kısa açıklama / Zorunlu - maks 150 karakter
    /// </summary>
    public string info1 { get; set; }
    public string info2 { get; set; }
    public string info3 { get; set; }

    /// <summary>
    /// Aktif Kullanıcı Sicil Numarası
    /// </summary>
    public string userInfo { get; set; }

    /// <summary>
    /// İlgili siparişin numarası
    /// </summary>
    public string orderNumber { get; set; }
    public string ReferanceKey { get; set; }
}

public class SmsMultipleMessageModel
{
    [LocalizedName("Module.Communication.Sms.GsmNo", "tr-TR")]
    public string number { get; set; }

    [LocalizedName("Module.Communication.Sms.Message", "tr-TR")]
    public string mesgbody { get; set; }
}

#endregion
