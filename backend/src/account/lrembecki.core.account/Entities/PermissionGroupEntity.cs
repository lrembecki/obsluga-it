using lrembecki.core.account.Dtos;
using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.account.Entities;

public class PermissionGroupEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<PermissionEntity> Permissions { get; private set; } = [];

    public static PermissionGroupEntity Create(Guid id, PermissionGroupDto model)
    {
        var entity = new PermissionGroupEntity()
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(PermissionGroupDto model)
    {
        Name = model.Name;
    }
}
