using MM.IT.Common.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs
{
    /// <summary>
    /// Hangfire Config Nesnesi -> appsettings.config: Hangfire eşleniği
    /// Scheduler Jobs Ayarlarını Saklar.
    /// </summary>
    public class HangfireConfigModel
    {
        /// <summary>
        /// Aktif mi Bilgisi
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Hangfire Datasının Bulunacağı Sql Connection Bilgisi
        /// </summary>
        public string DefaultConnection { get; set; }

        /// <summary>
        /// Kuyruk Yenilenme Sıklığı
        /// </summary>
        public int QueuePollInterval { get; set; }

        /// <summary>
        /// Timeout Bilgisi
        /// </summary>
        public int SlidingInvisibilityTimeout { get; set; }

        /// <summary>
        /// Uygulama Her Ayağa Kalktığında Database Schema Kontrolü Yapılsın Mı Bilgisi
        /// </summary>
        public bool IsSchemaGeneratorActive { get; set; }

        /// <summary>
        /// Dashboard Route Bilgisi
        /// </summary>
        public string DashboardRoute { get; set; }

        /// <summary>
        /// Dashboard Auth Kontrolü Aktiflik Durumu
        /// </summary>
        public bool IsAuthenticationActive { get; set; }

        /// <summary>
        /// Dashboard Yetki Kontrolü Aktiflik Durumu
        /// </summary>
        public bool IsAuthorizationActive { get; set; }

        /// <summary>
        /// Dashboard Başlığı
        /// </summary>
        public string DashboardTitle { get; set; }

        /// <summary>
        /// Hata Yaşadığında Mail Gönderilecek Adresler
        /// </summary>
        public HangfireEmailConfigsModel DefaultEmailConfigs { get; set; }
    }

    /// <summary>
    /// Hangfire Email Ayarları
    /// </summary>
    public class HangfireEmailConfigsModel
    {
        /// <summary>
        /// IOptions Injection için - Parameterless Constructor
        /// </summary>
        public HangfireEmailConfigsModel()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from">Kimden Bilgisi</param>
        /// <param name="to">Kime Bilgisi, ; ile birden fazla eklenebilir.</param>
        public HangfireEmailConfigsModel(EmailAdressModel from, string to)
        {
            From = from;
            To = to;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from">Kimden Bilgisi</param>
        /// <param name="to">Kime Bilgisi, ; ile birden fazla eklenebilir.</param>
        /// <param name="cc">CC Bilgisi, ; ile birden fazla eklenebilir.</param>
        public HangfireEmailConfigsModel(EmailAdressModel from, string to, string cc, bool isActive = true)
        {
            From = from;
            To = to;
            Cc = cc;
            IsActive = isActive;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="from">Kimden Bilgisi</param>
        /// <param name="to">Kime Bilgisi, ; ile birden fazla eklenebilir.</param>
        /// <param name="cc">CC Bilgisi, ; ile birden fazla eklenebilir.</param>
        /// <param name="bcc">Bcc Bilgisi, ; ile birden fazla eklenebilir.</param>
        public HangfireEmailConfigsModel(EmailAdressModel from, string to, string cc, string bcc, bool isActive = true)
        {
            From = from;
            To = to;
            Cc = cc;
            Bcc = bcc;
            IsActive = isActive;
        }

        /// <summary>
        /// Aktif Mi Bilgisi
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Kimden Bilgisi
        /// </summary>
        public EmailAdressModel From { get; set; }

        /// <summary>
        /// Kime Bilgisi, ; ile birden fazla eklenebilir.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// CC Bilgisi, ; ile birden fazla eklenebilir.
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// Bcc Bilgisi, ; ile birden fazla eklenebilir.
        /// </summary>
        public string Bcc { get; set; }
    }
}
