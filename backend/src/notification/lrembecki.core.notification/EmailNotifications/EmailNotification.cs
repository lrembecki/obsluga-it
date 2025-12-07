using lrembecki.core.Services;

namespace lrembecki.core.notification.EmailNotifications;

internal class EmailNotification : Notification
{
    public string Recipients { get; private set; } = null!;
    public string Subject { get; private set; } = null!;
}
