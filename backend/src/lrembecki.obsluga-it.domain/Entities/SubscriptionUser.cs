namespace lrembecki.obsluga_it.domain.Entities;

public class SubscriptionUser : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = default!;

    public static SubscriptionUser Create(Guid userId, Guid subscriptionId)
    {
        return new SubscriptionUser
        {
            UserId = userId,
            SubscriptionId = subscriptionId
        };
    }
}