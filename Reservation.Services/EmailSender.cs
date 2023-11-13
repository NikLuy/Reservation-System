using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Reservation.Models;

namespace Reservation.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly MailSettings _mailSettings;

    public EmailSender(IOptions<MailSettings> mailSettings,
                       ILogger<EmailSender> logger)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var request = new MailRequest()
        {
            Subject = subject,
            ToEmail = toEmail,
            Body = message
        };
        await SendEmailAsync(request);
    }

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.From.Add(InternetAddress.Parse(_mailSettings.Mail));
        if (_mailSettings.DebugMode)
        {
            email.To.Add(MailboxAddress.Parse(_mailSettings.DebugMail));
        }
        else
        {
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        }
        
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}
