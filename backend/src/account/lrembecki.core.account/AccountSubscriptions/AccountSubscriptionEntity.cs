using lrembecki.core.account.Accounts;
using lrembecki.core.account.PermissionGroups;
using lrembecki.core.Markers;
using lrembecki.core.shared;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.account.AccountSubscriptions;

public class AccountSubscriptionEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDefault { get; private set; }

    public Guid AccountId { get; private set; }
    public Guid SubscriptionId { get; private set; }

    public AccountEntity Account { get; private set; } = default!;
    public SubscriptionEntity Subscription { get; private set; } = default!;
    public List<PermissionGroupEntity> PermissionGroups { get; private set; } = [];

    public static AccountSubscriptionEntity Create(Guid id, AccountSubscriptionDto model)
        => new()
        {
            Id = id,
            IsActive = model.IsActive,
            IsDefault = model.IsDefault
        };

    public void Update(AccountSubscriptionDto model)
    {
        IsActive = model.IsActive;
        IsDefault = model.IsDefault;
    }
}
