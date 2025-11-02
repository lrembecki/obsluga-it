using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

public class PermissionGroupEntity : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<PermissionEntity> _permissions = [];
    public IReadOnlyCollection<PermissionEntity> Permissions => _permissions.ToList().AsReadOnly();

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public static PermissionGroupEntity Create(Guid id, string name)
        => new()
        {
            Id = id,
            Name = name
        };
    public void Update(string name)
    {
        Name = name;
    }

    public void AddPermission(PermissionEntity permission) => _permissions.Add(permission);
    public void ClearPermissions() => _permissions.Clear();
    public void RemovePermission(PermissionEntity permission) => _permissions.Remove(permission);
}
