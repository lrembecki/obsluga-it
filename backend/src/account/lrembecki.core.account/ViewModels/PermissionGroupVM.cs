using lrembecki.core.account.Entities;

namespace lrembecki.core.account.ViewModels;

public record PermissionGroupVM(
    Guid Id,
    string Name,
    ICollection<PermissionVM> Permissions
)
{ 
    internal static PermissionGroupVM Map(PermissionGroupEntity permissionGroupEntity)
    {
        if (permissionGroupEntity == null) return null!;
        
        return new PermissionGroupVM(
            permissionGroupEntity.Id, 
            permissionGroupEntity.Name,
            permissionGroupEntity.Permissions
                .Select(PermissionVM.Map)
                .ToList()
        );
    }
}
