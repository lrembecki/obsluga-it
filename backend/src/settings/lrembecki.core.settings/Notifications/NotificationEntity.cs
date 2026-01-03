using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.settings.Notifications;

internal class NotificationEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }

    public EmailNotificationEntity Email { get; private set; } = null!;

    public static NotificationEntity Create(Guid formDefinitionId, NotificationDto model)
    {
        var entity = new NotificationEntity
        {
            Id = formDefinitionId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(NotificationDto model)
    {
        if (model.Email  is not null)
        {
            Email ??= EmailNotificationEntity.Create(Id, model.Email);
            Email.Update(model.Email);
        }
        else
        {
            Email = null!;
        }
    }
}
