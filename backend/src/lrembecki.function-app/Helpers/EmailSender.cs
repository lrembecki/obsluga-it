using lrembecki.core.settings.ViewModels;
using Microsoft.Extensions.Logging;

using System.Net.Mail;

namespace lrembecki.function_app.Helpers;

internal interface IEmailSender
{
    Task SendEmailAsync(EmailWithPasswordVM email, EmailSender.Input input);
}

internal sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    public record Input(string[] To, string[] CC, string[] BCC, string Subject, string Body);
    public async Task SendEmailAsync(EmailWithPasswordVM email, Input input)
    {
        using var smtp = new SmtpClient(email.SmtpServer, email.SmtpPort);
        smtp.EnableSsl = true;
        smtp.Credentials = new System.Net.NetworkCredential(email.SmtpUsername, email.SmtpPassword);
        var mail = new MailMessage
        {
            From = new MailAddress(email.FromAddress, email.FromName),
            Subject = input.Subject,
            Body = input.Body,
            IsBodyHtml = true,
        };

        input.To.ToList().ForEach(mail.To.Add);
        input.CC.ToList().ForEach(mail.CC.Add);
        input.BCC.ToList().ForEach(mail.Bcc.Add);

        try
        {
            await smtp.SendMailAsync(mail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending smtp message");
            throw;
        }
    }
}
