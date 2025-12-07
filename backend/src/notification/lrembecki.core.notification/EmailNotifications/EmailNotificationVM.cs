using lrembecki.core.storage.ViewModels;

namespace lrembecki.core.notification.EmailNotifications;

public record EmailNotificationVM(
    Guid Id,
    DateTime? Sent,
    string Recipients,
    string Subject,
    StorageVM Body)
{
    internal static EmailNotificationVM Map(EmailNotification dto) =>
        new(
            dto.Id,
            dto.Sent,
            dto.Recipients,
            dto.Subject,
            StorageVM.Map(dto.Body)
        );
}