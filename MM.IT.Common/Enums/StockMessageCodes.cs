using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MM.IT.Common.Enums
{
    public enum StockMessageCodes
    {
        [Display(Name = "Başarılı!")]
        Successful = 1000,
        [Display(Name = "Kullanıcı adı alanı boş geçilemez!")]
        NotNullUsername = 1001,
        [Display(Name = "Kullanıcı şifre alanı boş geçilemez!")]
        NotNullPassword= 1002,
        [Display(Name = "Kayıt bulunamadı!")]
        NotFoundData = 1003,
        [Display(Name = "Sorgu sırasında hata oluştu!")]
        UnknownError = 1004,
        [Display(Name = "Lütfen tekrar deneyiniz. Sorun devam etmesi halinde Medimarkt IT ekibiyle görüşünüz!")]
        UnexpectedError = 1005,
        [Display(Name = "En az bir artikelId olmalıdır!")]
        NotNullArtikelIds = 1006,
        [Display(Name = "Artikel sayısı maksimum 1000 olmalıdır!")]
        ArtikelIdsCountExceeded = 1007,
        [Display(Name = "Store tablosunda veri bulunmamaktadır veya MPStatus aktif değildir!")]
        NotFountStore = 1008,
        [Display(Name = "T800 mağazası bulunmamaktadır veya MPStatus aktif değildir!")]
        NotFountT800Store = 1009,
        [Display(Name = "T601 mağazası bulunmamaktadır veya MPStatus aktif değildir!")]
        NotFountT601Store = 1010

    }
}

