using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Helpers.EmailHelper;

/// <summary>
/// Email Helper Interface Tanımı
/// </summary>
public interface IEmailHelper
{
    /// <summary>
    /// Default SMTP Server Ayarları ile Mail Gönderir.
    /// </summary>
    /// <param name="emailMessage">Email Mesaj Nesnesi</param>
    public void Send(EmailMessageModel emailMessage);
    public void SendWithAttachments(EmailMessageModel emailMessage, string folderName);

    /// <summary>
    /// Custom SMTP Server Ayarları ile Mail Gönderir.
    /// </summary>
    /// <param name="emailMessage">Email Mesajı Nesnesi</param>
    /// <param name="smtpConnection">Smtp Connection Nesnesi</param>
    public void Send(EmailMessageModel emailMessage, SmtpConnectionModel smtpConnection);


}