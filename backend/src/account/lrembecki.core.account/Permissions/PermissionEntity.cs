using lrembecki.core.Markers;
using lrembecki.core.shared;

namespace lrembecki.core.account.Permissions;

public class PermissionEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public static PermissionEntity Create(Guid id, PermissionDto model)
        => new()
        {
            Id = id,
            Name = model.Name
        };

    public void Update(PermissionDto model)
    {
        Name = model.Name;
    }
}
