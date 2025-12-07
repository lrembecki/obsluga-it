using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.storage.Entities;

namespace lrembecki.core.notification;

internal class Notification : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public DateTime? Sent { get; private set; }
    public Guid BodyId { get; private set; }
    public StorageEntity Body { get; private set; } = null!;

    public void MarkAsSent()
    {
        Sent = DateTime.UtcNow;
    }
}
