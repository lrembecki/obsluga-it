using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

public class AccountSubscriptionEntity : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<PermissionGroupEntity> _permissionGroups = [];
    public IReadOnlyCollection<PermissionGroupEntity> PermissionGroups => _permissionGroups.ToList().AsReadOnly();

    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid SubscriptionId { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDefault { get; private set; }

    public static AccountSubscriptionEntity Create(Guid id, Guid accountId, Guid subscriptionId)
        => new()
        {
            Id = id,
            AccountId = accountId,
            SubscriptionId = subscriptionId,
            IsActive = true,
            IsDefault = false
        };

    public void AddPermissionGroup(PermissionGroupEntity permissionGroupEntity) => _permissionGroups.Add(permissionGroupEntity);
    public void ClearPermissionGroups() => _permissionGroups.Clear();
    public void RemovePermissionGroup(PermissionGroupEntity permissionGroupEntity) => _permissionGroups.Remove(permissionGroupEntity);

    public void Update(bool isActive, bool isDefault)
    {
        IsActive = isActive;
        IsDefault = isDefault;
    }
}
