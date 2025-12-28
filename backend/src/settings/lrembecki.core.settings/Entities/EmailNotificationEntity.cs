using lrembecki.core.settings.Dtos;

namespace lrembecki.core.settings.Entities;

internal class EmailNotificationEntity
{
    public Guid NotificationId { get; private set; }

    public Guid EmailId { get; private set; }
    public EmailEntity Email { get; private set; } = null!;

    public Guid EmailTemplateId { get; private set; }
    public EmailTemplateEntity EmailTemplate { get; private set; } = null!;

    public static EmailNotificationEntity Create(Guid notificationId, EmailNotificationDto model)
    {
        var entity = new EmailNotificationEntity
        {
            NotificationId = notificationId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(EmailNotificationDto model)
    {
        EmailId = model.EmailId;
        EmailTemplateId = model.EmailTemplateId;
    }

}
