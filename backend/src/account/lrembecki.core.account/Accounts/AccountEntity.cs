using lrembecki.core.account.PermissionGroups;
using lrembecki.core.Markers;
using lrembecki.core.shared;

namespace lrembecki.core.account.Accounts;

public class AccountEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public List<PermissionGroupEntity> PermissionGroups { get; private set; } = [];

    public static AccountEntity Create(Guid id, AccountDto model)
        => new()
        {
            Id = id,
            IsActive = true,
            Email = model.Email
        };

    public void Update(AccountDto model) => Email = model.Email;
}
