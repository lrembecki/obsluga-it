using lrembecki.core.Markers;

namespace lrembecki.core.Entities
{
    public class SubscriptionBaseEntity : BaseEntity, IHasSubscriptionId
    {
        public Guid SubscriptionId { get; protected set; }
    }
}
