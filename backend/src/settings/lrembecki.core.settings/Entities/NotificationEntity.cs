using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Dtos;

namespace lrembecki.core.settings.Entities;

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
