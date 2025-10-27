using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class UserSubscriptionEntity : SubscriptionBaseEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public static UserSubscriptionEntity Create(UserEntity user, bool isDefault)
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