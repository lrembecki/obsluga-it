using lrembecki.core.settings.Entities;

namespace lrembecki.core.settings.ViewModels;

public record EmailVM(
    Guid Id,
    bool IsActive,
    string SmtpServer,
    int SmtpPort,
    string SmtpUsername,
    string SmtpPassword,
    string FromAddress,
    string FromName,
    string ReplyToAddress,
    string ReplyToName)
{
    public static EmailVM Map(EmailEntity email)
    {
        if (email == null) return null!;

        return new(
            Id: email.Id,
            IsActive: email.IsActive,
            SmtpServer: email.SmtpServer,
            SmtpPort: email.SmtpPort,
            SmtpUsername: email.SmtpUsername,
            SmtpPassword: email.SmtpPassword,
            FromAddress: email.FromAddress,
            FromName: email.FromName,
            ReplyToAddress: email.ReplyToAddress,
            ReplyToName: email.ReplyToName
        );
    }
}
