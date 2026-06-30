using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms
{
    public class SMSSenderRequest
    {
        //[Required(ErrorMessage = "Mesgbody alanı boş bırakılamaz!")]
        public string Mesgbody { get; set; }
        //[Required(ErrorMessage = "Numbers alanı boş bırakılamaz!")]
        public string Numbers { get; set; }
        //[Required(ErrorMessage = "ChannelCode alanı boş bırakılamaz!")]
        public string ChannelCode { get; set; }
        public string MessageDescription { get; set; }
        ///// <summary>
        ///// SMS gönderimi için kullanılacak tanımlı operatör veya hesap bilgisi
        ///// </summary>
        //public string AccountId { get; set; }
        ///// <summary>
        ///// Gönderen başlığı, eğer belirtilmez ise hesabınıza tanımlı ilk alfanumerik bilgisi ile gönderim yapılacaktır.
        ///// </summary>
        //public string Originator { get; set; }
        ///// <summary>
        ///// Bilgilendirme SMS lerinde Mobildev de bulunan bilacklist kontrol edilmek istenirse  1 Kontrol Aktif - 0 Kontrol Pasif
        ///// </summary>
        //public string Blacklist { get; set; }
        ///// <summary>
        /// ddMMyyyyHHmm sms gönderiminin başlayacağı başlangıç tarihi, Eğer değer belirtilmezse gönderime hemen başlayacaktır.
        /// </summary>
        public string SDate { get; set; }
        /// <summary>
        /// ddMMyyyyHHmm sms gönderiminin operatör tarafında sonlandırılacağı tarih, Sdate ile arasında en az 1 saat fark olmalı. Boş bırakılır veya XML düğümüne eklenmez ise otomatik olarak operatör standartları geçerli olacaktır
        /// </summary>
        public string EDate { get; set; }
        ///// <summary>
        ///// 0 – Standart 1 – Türkçe Karakterli gönderim.Türkçe SMS gönderiminde SMS uzunlukları ve ücretlendirme ile ilgili bilgiler operatörler ile imzaladığınız sözleşmelerde yer almaktadır. Türkçe karakterli ve bir sms den uzun olan (concat) mesajlarda son parçanın uzunluğu 3 karakterden az olamaz.
        ///// </summary>
        //public int Encoding { get; set; }
        /// <summary>
        /// N / C N: Bilgilendirme (notification) mesajları için kullanılır, C: Kampanya / Reklam mesajı gönderimi
        /// </summary>
        //[Required(ErrorMessage = "MessageType alanı boş bırakılamaz!")]
        public string MessageType { get; set; }
        /// <summary>
        /// B : Bireysel T: Tacir MessageType C ise Zorunlu
        /// </summary>
        public string RecipientType { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public bool IsSecretData { get; set; }
        public string UserInfo { get; set; }
        public string OrderNumber { get; set; }
    }
}
