using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common;
/// <summary>
/// Email gönderimi için kullanılacak model nesnesi
/// </summary>
public class EmailMessageModel
{
    /// <summary>
    /// Custom Mail Mesaj Nesnesini Oluşturur. 
    /// Bu şekilde kullanımda Parametreler Custom Olarak Set Edilmeli.
    /// </summary>
    public EmailMessageModel()
    {

    }

    /// <summary>
    /// Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden:';' ile birden fazla gönderilebilir.</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(string from, string to, string subject, string content)
    {
        From = StringToEmailAddressModel(from);
        To = StringToEmailAddressModel(to);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    /// Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden:';' ile birden fazla gönderilebilir.</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="cc">Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(string from, string to, string cc, string subject, string content)
    {
        From = StringToEmailAddressModel(from);
        To = StringToEmailAddressModel(to);
        Cc = StringToEmailAddressModel(cc);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    /// Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden:';' ile birden fazla gönderilebilir.</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="cc">Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="bcc">Gizli Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(string from, string to, string cc, string bcc, string subject, string content)
    {
        From = StringToEmailAddressModel(from);
        To = StringToEmailAddressModel(to);
        Cc = StringToEmailAddressModel(cc);
        Bcc = StringToEmailAddressModel(bcc);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, string to, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = StringToEmailAddressModel(to);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="cc">Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, string to, string cc, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = StringToEmailAddressModel(to);
        Cc = StringToEmailAddressModel(cc);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime:';' ile birden fazla gönderilebilir.</param>
    /// <param name="cc">Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="bcc">Gizli Bilgi:';' ile birden fazla gönderilebilir.</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, string to, string cc, string bcc, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = StringToEmailAddressModel(to);
        Cc = StringToEmailAddressModel(cc);
        Bcc = StringToEmailAddressModel(bcc);
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime  Mail Adresi Nesnesi</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, EmailAdressModel to, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = new List<EmailAdressModel> { to };
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime Mail Adresi Nesnesi</param>
    /// <param name="cc">Bilgi Mail Adresi Nesnesi</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, EmailAdressModel to, EmailAdressModel cc, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = new List<EmailAdressModel> { to };
        Cc = new List<EmailAdressModel> { cc };
        Subject = subject;
        Content = content;
    }

    /// <summary>
    ///  Mail Mesaj Nesnesini Oluşturur.
    /// </summary>
    /// <param name="from">Kimden Mail Adresi Nesnesi</param>
    /// <param name="to">Kime Mail Adresi Nesnesi</param>
    /// <param name="cc">Bilgi Mail Adresi Nesnesi</param>
    /// <param name="bcc">Gizli Bilgi Mail Adresi Nesnesi</param>
    /// <param name="subject">Konu</param>
    /// <param name="content">İçerik</param>
    public EmailMessageModel(EmailAdressModel from, EmailAdressModel to, EmailAdressModel cc, EmailAdressModel bcc, string subject, string content)
    {
        From = new List<EmailAdressModel> { from };
        To = new List<EmailAdressModel> { to };
        Cc = new List<EmailAdressModel> { cc };
        Bcc = new List<EmailAdressModel> { bcc };
        Subject = subject;
        Content = content;
    }

    /// <summary>
    /// Kimden Gideceği Bilgisi
    /// </summary>
    public IEnumerable<EmailAdressModel> From { get; set; }

    /// <summary>
    /// Kime Gideceği Bilgisi
    /// </summary>
    public IEnumerable<EmailAdressModel> To { get; set; }

    /// <summary>
    /// CC'de Kimin Olacağı Bilgisi
    /// </summary>
    public IEnumerable<EmailAdressModel> Cc { get; set; }

    /// <summary>
    /// BCC'de Kimin Olacağı Bilgisi
    /// </summary>
    public IEnumerable<EmailAdressModel> Bcc { get; set; }

    /// <summary>
    /// FileStream Ekleri
    /// </summary>
    public IEnumerable<FileStream> FileStreamAttachments { get; set; }

    /// <summary>
    /// HTTP Request ile gelen Ekler
    /// </summary>
    public IFormFileCollection FormFileAttachments { get; set; }

    /// <summary>
    /// Konu Bilgisi
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// İçerik Bilgisi
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Html Template Bilgisi
    /// </summary>
    public string Template { get; set; }


    private IEnumerable<EmailAdressModel> StringToEmailAddressModel(string theString, char splitChar = ';')
    {
        if (string.IsNullOrWhiteSpace(theString))
        {
            return new List<EmailAdressModel>();
        }

        return theString
            .Split(splitChar)
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => new EmailAdressModel(p.Trim(), p.Trim())).ToList();
    }
}

/// <summary>
/// Email Adresi Model Nesnesi
/// </summary>
public class EmailAdressModel
{
    /// <summary>
    /// Parameterless Constructor -> IOptions kullanımı için.
    /// </summary>
    public EmailAdressModel()
    {

    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="address"></param>
    public EmailAdressModel(string name, string address)
    {
        Name = name;
        Address = address;
    }

    /// <summary>
    /// Görünecek Ad
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Adres Bilgisi
    /// </summary>
    public string Address { get; set; }
}
