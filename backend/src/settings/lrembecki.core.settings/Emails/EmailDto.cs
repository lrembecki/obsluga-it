using lrembecki.core.settings.EmailTemplates;

namespace lrembecki.core.settings.Emails;

public record EmailDto
{
    public bool IsActive { get; set; }
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string ReplyToAddress { get; set; } = string.Empty;
    public string ReplyToName { get; set; } = string.Empty;
    public EmailTemplateDto Template { get; set; } = null!;
}
