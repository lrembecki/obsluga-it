using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.trotamundos.Common;

public class TrotamundosBaseEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; protected set; }
}
