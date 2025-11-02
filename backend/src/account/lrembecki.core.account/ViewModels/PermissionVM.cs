using lrembecki.core.account.Entities;

namespace lrembecki.core.account.ViewModels;

public record PermissionVM(
    Guid Id, 
    string Name
)
{
    internal static PermissionVM Map(PermissionEntity permissionEntity)
    {
        if (permissionEntity == null) return null!;

        return new PermissionVM(permissionEntity.Id, permissionEntity.Name);
    }
}
