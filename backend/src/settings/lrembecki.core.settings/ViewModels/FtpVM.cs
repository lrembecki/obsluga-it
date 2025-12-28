using lrembecki.core.settings.Entities;

namespace lrembecki.core.settings.ViewModels;

public record FtpVM(
    
    Guid Id,
    bool IsActive,
    string Server,
    int Port,
    string Username,
    string Password,
    string RemotePath,
    bool UseSsl)
{
    internal static FtpVM Map(EmailEntity email)
    {
        if (email == null) return null!;
        return new(
            Id: email.Id,
            IsActive: email.IsActive,
            Server: email.SmtpServer,
            Port: email.SmtpPort,
            Username: email.SmtpUsername,
            Password: email.SmtpPassword,
            RemotePath: string.Empty,
            UseSsl: true
        );
    }
}
