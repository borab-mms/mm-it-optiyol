using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MM.IT.Common.Enums
{
    public enum MarketPlaceMessageCodes
    {
        [Display(Name = "Başarılı!")]
        Successful = 1000,
        [Display(Name = "Sku alanı boş geçilemez!")]
        NotNullSku = 1001,
        [Display(Name = "Channel alanı boş geçilemez!")]
        NotNullChannel = 1002,
        [Display(Name = "Kayıt bulunamadı!")]
        NotFoundData = 1003,
        [Display(Name = "Sorgu sırasında hata oluştu!")]
        UnknownError = 1004,
        [Display(Name = "Lütfen tekrar deneyiniz. Sorun devam etmesi halinde Medimarkt IT ekibiyle görüşünüz!")]
        UnexpectedError = 1005,
        [Display(Name = "Amount alanı boş geçilemez!")]
        NotNullAmount = 1006,
        [Display(Name = "return_reason alanı boş geçilemez!")]
        NotNullReturnReason = 1007,
        [Display(Name = "return_date alanı boş geçilemez!")]
        NotNullReturnDate = 1008,
        [Display(Name = "return_id alanı boş geçilemez!")]
        NotNullReturnId = 1009,
        [Display(Name = "return_detail_id alanı boş geçilemez!")]
        NotNullReturnDetailId= 1010,
        [Display(Name = "shipping_company alanı boş geçilemez!")]
        NotNullShippingCompany = 1011,
        [Display(Name = "cargo_code alanı boş geçilemez!")]
        NotNullCargoCode = 1012,
    }
}
