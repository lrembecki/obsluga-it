using lrembecki.core.account.Entities;

namespace lrembecki.core.account.ViewModels;

public record AccountVM(
    Guid Id,
    string Email,
    ICollection<PermissionGroupVM> PermissionGroups
)
{
    public static AccountVM Map(AccountEntity accountEntity)
    {
        if (accountEntity == null) return null!;

        return new AccountVM(
            accountEntity.Id, 
            accountEntity.Email,
            accountEntity.PermissionGroups
                .Select(PermissionGroupVM.Map)
                .ToList()
        );
    }
}
