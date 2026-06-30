using Microsoft.Extensions.Options;
using MimeKit;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace MM.IT.Common.Helpers.EmailHelper;

/// <summary>
/// IEmailHelper şartlarını barındıran Email Helper Nesnesi
/// </summary>
public class EmailHelper : IEmailHelper
{
    private readonly SmtpConnectionModel _smtpConnection;

    public EmailHelper(IOptions<SmtpConnectionConfigModel> smtpConfigs)
    {
        _smtpConnection = smtpConfigs.Value.DefaultConnection;
    }

    //public void Send(EmailMessageModel emailMessage)
    //{
    //    var mimeMessage = GenerateMimeMessageFromModel(emailMessage, _smtpConnection);

    //    using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
    //    {
    //        emailClient.Connect(_smtpConnection.Host, _smtpConnection.Port, _smtpConnection.UseSsl);

    //        if (_smtpConnection.IsAuthenticationEnabled)
    //        {
    //            emailClient.Authenticate(_smtpConnection.Username, _smtpConnection.Password);
    //        }

    //        emailClient.Send(mimeMessage);
    //        emailClient.Disconnect(true);

    //        if (emailMessage.FileStreamAttachments != null && emailMessage.FileStreamAttachments.Any())
    //        {
    //            foreach (var fileStream in emailMessage.FileStreamAttachments)
    //            {
    //                fileStream.Close();
    //                fileStream.Dispose();
    //            }
    //        }
    //    }
    //}
    public void Send(EmailMessageModel emailMessage)
    {
        try
        {

            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = _smtpConnection.Host;
            mail.Port = Convert.ToInt32(_smtpConnection.Port);
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Credentials = CredentialCache.DefaultNetworkCredentials;

            MailMessage mailMessage = new MailMessage();
            mailMessage.Priority = MailPriority.High;
            mailMessage.From = new MailAddress(emailMessage.From.Select(a => a.Address).FirstOrDefault());

            foreach (var emailAddress in emailMessage.To)
                mailMessage.To.Add(emailAddress.Address);

            if (emailMessage.Cc.Any())
            {
                foreach (var item in emailMessage.Cc)
                {
                    mailMessage.CC.Add(item.Address);
                }
            }

            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = emailMessage.Content;

            mail.Send(mailMessage);
        }
        catch (Exception ex)
        {
        }
    }
    public void SendWithAttachments(EmailMessageModel emailMessage,string folderName)
    {
        try
        {

            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            mail.Host = _smtpConnection.Host;
            mail.Port = Convert.ToInt32(_smtpConnection.Port);
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Credentials = CredentialCache.DefaultNetworkCredentials;

            MailMessage mailMessage = new MailMessage();
            mailMessage.Priority = MailPriority.High;
            mailMessage.From = new MailAddress(emailMessage.From.Select(a => a.Address).FirstOrDefault());

            foreach (var emailAddress in emailMessage.To)
                mailMessage.To.Add(emailAddress.Address);

            if (emailMessage.Cc.Any())
            {
                foreach (var item in emailMessage.Cc)
                {
                    mailMessage.CC.Add(item.Address);
                }
            }

            mailMessage.Subject = emailMessage.Subject;
            mailMessage.Body = emailMessage.Content;

            var path = Directory.GetCurrentDirectory() + "\\Files\\" + $"{folderName}";
            mailMessage.Attachments.Add(new Attachment(path));

            mail.Send(mailMessage);
        }
        catch (Exception ex)
        {
        }
    }
    public void Send(EmailMessageModel emailMessage, SmtpConnectionModel smtpConnection)
    {
        var mimeMessage = GenerateMimeMessageFromModel(emailMessage, smtpConnection);

        using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
        {
            emailClient.Connect(smtpConnection.Host, smtpConnection.Port, smtpConnection.UseSsl);

            if (smtpConnection.IsAuthenticationEnabled)
            {
                emailClient.Authenticate(smtpConnection.Username, smtpConnection.Password);
            }

            emailClient.Send(mimeMessage);
            emailClient.Disconnect(true);

            if (emailMessage.FileStreamAttachments != null && emailMessage.FileStreamAttachments.Any())
            {
                foreach (var fileStream in emailMessage.FileStreamAttachments)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }
    }

    private MimeMessage GenerateMimeMessageFromModel(EmailMessageModel emailMessage, SmtpConnectionModel smtpConnection)
    {
        var mimeMessage = new MimeMessage
        {
            Subject = emailMessage.Subject
        };

        mimeMessage.From.AddRange(emailMessage.From.Select(p => new MailboxAddress(p.Name, p.Address)));
        mimeMessage.To.AddRange(emailMessage.To.Select(p => new MailboxAddress(p.Name, p.Address)));

        if (emailMessage.Cc != null && emailMessage.Cc.Any())
        {
            mimeMessage.Cc.AddRange(emailMessage.Cc.Select(p => new MailboxAddress(p.Name, p.Address)));
        }

        if (emailMessage.Bcc != null && emailMessage.Bcc.Any())
        {
            mimeMessage.Bcc.AddRange(emailMessage.Bcc.Select(p => new MailboxAddress(p.Name, p.Address)));
        }

        var bodyBuilder = new BodyBuilder();

        if (!string.IsNullOrWhiteSpace(emailMessage.Template) || smtpConnection.UseTemplate)
        {
            var appDomain = System.AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            var templateName = string.IsNullOrWhiteSpace(emailMessage.Template) ? smtpConnection.DefaultTemplate : emailMessage.Template;

            var filePath = Path.Combine(basePath, @"Helpers\EmailHelper\Templates", templateName);

            using (var reader = File.OpenText(filePath))
            {
                bodyBuilder.HtmlBody = reader.ReadToEnd().Replace("@HTMLCONTENT", emailMessage.Content);
            }
        }
        else
        {
            bodyBuilder.HtmlBody = emailMessage.Content;
        }

        if (emailMessage.FileStreamAttachments != null && emailMessage.FileStreamAttachments.Any())
        {
            foreach (var attachment in emailMessage.FileStreamAttachments)
            {
                bodyBuilder.Attachments.Add(attachment.Name, attachment);
            }
        }

        if (emailMessage.FormFileAttachments != null && emailMessage.FormFileAttachments.Any())
        {

            byte[] fileBytes;
            foreach (var attachment in emailMessage.FormFileAttachments)
            {
                using (var ms = new MemoryStream())
                {
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, MimeKit.ContentType.Parse(attachment.ContentType));
            }
        }

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return mimeMessage;
    }

}
