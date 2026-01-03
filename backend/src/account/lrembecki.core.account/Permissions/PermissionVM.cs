namespace lrembecki.core.account.Permissions;

public record PermissionVM(
    Guid Id, 
    string Name
)
{
    public static PermissionVM Map(PermissionEntity permissionEntity)
    {
        if (permissionEntity == null) return null!;

        return new PermissionVM(permissionEntity.Id, permissionEntity.Name);
    }
}
