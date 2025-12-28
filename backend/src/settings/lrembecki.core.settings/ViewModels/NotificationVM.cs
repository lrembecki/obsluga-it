using lrembecki.core.settings.Entities;

namespace lrembecki.core.settings.ViewModels;

public record NotificationVM(
    Guid Id,
    EmailNotificationVM Email
)
{
    internal static NotificationVM Map(NotificationEntity entity)
    {
        if (entity == null) return null!;

        return new NotificationVM(
            entity.Id,
            EmailNotificationVM.Map(entity.Email)
        );
    }
}
