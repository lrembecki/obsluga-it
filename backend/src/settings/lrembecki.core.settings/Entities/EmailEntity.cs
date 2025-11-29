using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Dtos;

namespace lrembecki.core.settings.Entities;

public class EmailEntity : SubscriptionBaseEntity, IHasId<Guid>, IHasActive
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public string SmtpServer { get; private set; } = string.Empty;
    public int SmtpPort { get; private set; }
    public string SmtpUsername { get; private set; } = string.Empty;
    public string SmtpPassword { get; private set; } = string.Empty;
    public string FromAddress { get; private set; } = string.Empty;
    public string FromName { get; private set; } = string.Empty;
    public string ReplyToAddress { get; private set; } = string.Empty;
    public string ReplyToName { get; private set; } = string.Empty;

    public static EmailEntity Create(Guid id, EmailDto model)
    {
        var entity = new EmailEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(EmailDto model)
    {
        IsActive = model.IsActive;
        SmtpServer = model.SmtpServer;
        SmtpPort = model.SmtpPort;
        SmtpUsername = model.SmtpUsername;
        SmtpPassword = model.SmtpPassword;
        FromAddress = model.FromAddress;
        FromName = model.FromName;
        ReplyToAddress = model.ReplyToAddress;
        ReplyToName = model.ReplyToName;
    }
}
