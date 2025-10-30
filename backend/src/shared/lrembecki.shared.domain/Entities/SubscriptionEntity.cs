using lrembecki.shared.domain.Abstractions;

namespace lrembecki.shared.domain.Entities;

public class SubscriptionEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static SubscriptionEntity Create(Guid id, string name)
    {
        var subscription = new SubscriptionEntity
        {
            Id = id,
            Name = name
        };

        return subscription;
    }
}