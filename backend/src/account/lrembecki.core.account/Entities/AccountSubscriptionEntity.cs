using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

public class AccountSubscriptionEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDefault { get; private set; }
    public List<PermissionGroupEntity> PermissionGroups { get; private set; } = [];

    public static AccountSubscriptionEntity Create(Guid id, Guid accountId, Guid subscriptionId)
        => new()
        {
            Id = id,
            AccountId = accountId,
            SubscriptionId = subscriptionId,
            IsActive = true,
            IsDefault = false
        };

    public void Update(bool isActive, bool isDefault)
    {
        IsActive = isActive;
        IsDefault = isDefault;
    }
}
