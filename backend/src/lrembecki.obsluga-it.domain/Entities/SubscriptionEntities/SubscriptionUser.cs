namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

public class SubscriptionUser : SubscriptionEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public static SubscriptionUser Create(User user, bool isDefault)
        => new ()
        {
            UserId = user.Id,
            User = user,
            IsActive = true,
            IsDefault = isDefault
        };

    public void Update(bool isDefault, bool isActive)
    {
        IsDefault = isDefault;
        IsActive = isActive;
    }
}