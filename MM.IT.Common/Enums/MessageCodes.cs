using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MM.IT.Common.Enums
{
    public enum MessageCodes
    {
        [Display(Name = "Geçersiz login")]
        GecersizLogin = 996,
        [Display(Name = "Geçersiz kimlik bilgileri")]
        GecersizKimlikBilgileri = 997,
        [Display(Name = "Geçersiz Model")]
        GecersizModel = 998,
        [Display(Name = "Mağaza bulunamadı!")]
        NotFountStore = 1030,
        [Display(Name = "Mağaza kapalı!")]
        StoreClosed = 1031,
        [Display(Name = "En fazla 3 artikel girilmelidir!")]
        TheProductLimitExceeded = 1032,

        /***********************SMSSender***********************/
        [Display(Name = "Başarılı!")]
        Successful = 1000,
        [Display(Name = "MessageType alanı N veya C olmalıdır!")]
        InvalidMessageType = 1001,
        [Display(Name = "RecipientType alanı B veya T olmalıdır!")]
        InvalidRecipientType = 1002,
        [Display(Name = "Sorgu sırasında hata oluştu!")]
        UnknownError = 1003,
        [Display(Name = "RecipientType alanı boş olamaz!")]
        NotNullRecipientType = 1004,
        [Display(Name = "ChannelCode sistemde bulunmuyor!")]
        NothingChannelName = 1005,
        [Display(Name = "Numara sayısı 3000 ü geçemez!")]
        LimitExceededOfNumbers = 1006,
        [Display(Name = "Mesgbody alanı boş olamaz!")]
        NotNullMesgbody = 1007,
        [Display(Name = "SDate alanı boş olamaz!")]
        NotNullSDate = 1008,
        [Display(Name = "SDate tarih formatı hatalı!")]
        WrongFormatTheSDate = 1009,
        [Display(Name = "Bir(1) saatten eski tarihli SMS'ler gönderilemez!")]
        OldSDate = 1010,
        [Display(Name = "EDate tarih formatı hatalı!")]
        WrongFormatTheEDate = 1011,
        [Display(Name = "EDate, SDate'den küçük olamaz!")]
        EDateCannotBeLessSDate = 1012,
        [Display(Name = "EDate ve SDate arasında en fazla 72 saat olmalıdır!")]
        NoMoreThan72HoursBetweenEDateAndSDate = 1013,
        [Display(Name = "MessageType alanı boş olamaz!")]
        NotNullMessageType = 1014,
        [Display(Name = "ChannelCode alanı boş olamaz!")]
        NotNullChannelCode = 1015,
        [Display(Name = "Numbers alanı boş olamaz!")]
        NotNullNumbers = 1016,
        [Display(Name = "Lütfen tekrar deneyiniz. Sorun devam etmesi halinde Medimarkt IT ekibiyle görüşünüz!")]
        UnexpectedError = 1017,
        [Display(Name = "Mesaj açıklama alanı 50 karakterden fazla olamaz!")]
        CharacterCountExceeded = 1018,
        [Display(Name = "Mesaj alanı 400 karakterden fazla olamaz!")]
        MessageLengthExceeded = 1019,
        [Display(Name = "UserInfo alanı 30 karakterden fazla olamaz!")]
        MessageLengthUserInfo = 1022,
        [Display(Name = "Mesgbody alanı 894 karakterden fazla olamaz!")]
        MessageLengthMesgbody = 1023,
        [Display(Name = "OrderNumber alanı 10 karakterden fazla 7 karakterden az olamaz!")]
        OrderNumberLengthMesgbody = 1024,
        [Display(Name = "Telefon numarası 10 karakterden az olamaz!")]
        PhoneCharackterLess = 1025,


        /***********************EmailSender**********************/
        [Display(Name = "Mail gönderim sırasında hata ortaya çıktı!")]
        NotSentEmail = 1020,
        /***********************EmailSender**********************/
        [Display(Name = "Artikel sayısı 200'ü geçemez!")]
        ProductCountExceeded = 1021,

        #region MMPayment

        [Display(Name = "Redis server ile bağlantı kurulmadı!")]
        RedisServerConnectError = 1050,
        [Display(Name = "Redis bilgileri veritabanına işlenmedi!")]
        NotSaveDataToDb = 1051,
        [Display(Name = "Redis datası 1 state'den 2 state'e geçmedi!")]
        RedisUnSuccesfullData = 1052,

        #endregion
    }
}
