using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

public class PermissionGroupEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<PermissionEntity> Permissions { get; private set; } = [];

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
}
