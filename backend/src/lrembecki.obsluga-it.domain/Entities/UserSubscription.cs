namespace lrembecki.obsluga_it.domain.Entities;

public class UserSubscription : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = default!;

    public static UserSubscription Create(Guid userId, Guid subscriptionId)
    {
        return new UserSubscription
        {
            UserId = userId,
            SubscriptionId = subscriptionId
        };
    }
}