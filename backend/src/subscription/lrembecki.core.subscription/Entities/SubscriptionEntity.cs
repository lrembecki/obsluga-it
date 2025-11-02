using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.subscription.Entities;

public class SubscriptionEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public static SubscriptionEntity Create(Guid id, string name)
        => new SubscriptionEntity
        {
            Id = id,
            Name = name
        };

    public void Update(string name)
    {
        Name = name;
    }
}
