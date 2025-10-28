using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class SubscriptionUserEntity : SubscriptionBaseEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    // UPDATED: include subscription to ensure composite key {SubscriptionId, UserId} has values
    public static SubscriptionUserEntity Create(Guid id, UserEntity user, SubscriptionEntity subscription, bool isDefault)
        => new ()
        {
            Id = id,
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