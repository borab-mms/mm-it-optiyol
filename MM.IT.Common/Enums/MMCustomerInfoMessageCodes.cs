using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Enums
{
    public enum MMCustomerInfoMessageCodes
    {
        [Display(Name = "Başarılı!")]
        Successful = 1000,
        [Display(Name = "Kullanıcı adı alanı boş geçilemez!")]
        NotNullUsername = 1001,
        [Display(Name = "Şifre şifre alanı boş geçilemez!")]
        NotNullPassword= 1002,
        [Display(Name = "referenceType ve referenceValue alanları tek olarak kullanılamaz iksinin dolu veya boş olması gerekiyor!")]
        NotNullReferenceTypeOReferenceValue = 1003,
        [Display(Name = "Kullanıcı adı veya şifre yalnış!")]
        WrongUsernameOrPassword = 1004,
        [Display(Name = "Mobile, email alanlarından biri veya referenceType/referenceValue alanların ikisi zorunlu olmalıdır!")]
        NotNullMobileOrEmailReferenceTypeOReferenceValue = 1005,
        [Display(Name = "Sorgu sırasında hata oluştu!")]
        UnknownError = 1006,
        [Display(Name = "Kayıt bulunamadı!")]
        NotFoundData = 1007,
        [Display(Name = "Telefon numarası maximum 16 karakter olmalıdır!")]
        ExceedMobile = 1008
    }
}
