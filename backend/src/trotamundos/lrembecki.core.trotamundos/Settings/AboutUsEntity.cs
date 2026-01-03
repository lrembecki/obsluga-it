using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Settings;

internal class AboutUsEntity : TrotamundosBaseEntity
{
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public static AboutUsEntity Create(Guid id, AboutUsDto model)
    {
        var entity = new AboutUsEntity
        {
            Id = id,
        };
        entity.Update(model);
        return entity;
    }
    public void Update(AboutUsDto model)
    {
        ImageId = model.ImageId;
        Title = model.Title;
        Description = model.Description;
    }
}
