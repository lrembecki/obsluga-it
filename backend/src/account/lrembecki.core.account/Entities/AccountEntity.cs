using lrembecki.core.account.Dtos;
using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

internal class AccountEntity : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<PermissionGroupEntity> _permissionGroups = [];
    public IReadOnlyCollection<PermissionGroupEntity> PermissionGroups => _permissionGroups.ToList().AsReadOnly();

    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public string Email { get; private set; } = string.Empty;

    public static AccountEntity Create(Guid id, AccountDto model)
        => new()
        {
            Id = id,
            IsActive = true,
            Email = model.Email
        };
    public void AddPermissionGroup(PermissionGroupEntity permissionGroupEntity) => _permissionGroups.Add(permissionGroupEntity);
    public void ClearPermissionGroups() => _permissionGroups.Clear();
    public void RemovePermissionGroup(PermissionGroupEntity permissionGroupEntity) => _permissionGroups.Remove(permissionGroupEntity);
    public void Update(AccountDto model) => Email = model.Email;
}
