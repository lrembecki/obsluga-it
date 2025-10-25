namespace lrembecki.obsluga_it.domain.Entities;

public class SubscriptionUser : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = default!;

    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public static SubscriptionUser Create(User user, Subscription subscription, bool isDefault)
        => new ()
        {
            UserId = user.Id,
            User = user,
            SubscriptionId = subscription.Id,
            Subscription = subscription,
            IsActive = true,
            IsDefault = isDefault
        };

    public void Update(bool isDefault, bool isActive)
    {
        IsDefault = isDefault;
        IsActive = isActive;
    }
}