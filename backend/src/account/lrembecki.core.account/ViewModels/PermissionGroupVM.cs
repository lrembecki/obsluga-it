using lrembecki.core.account.Entities;

namespace lrembecki.core.account.ViewModels;

public record PermissionGroupVM(
    Guid Id,
    string Name,
    ICollection<Guid> Permissions
)
{ 
    public static PermissionGroupVM Map(PermissionGroupEntity permissionGroupEntity)
    {
        if (permissionGroupEntity == null) return null!;
        
        return new PermissionGroupVM(
            permissionGroupEntity.Id, 
            permissionGroupEntity.Name,
            permissionGroupEntity.Permissions
                .Select(e => e.Id)
                .ToList()
        );
    }
}
