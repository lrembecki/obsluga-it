using lrembecki.core.Events;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

internal class AboutUsEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;
    public List<AboutUsItemEntity> Items { get; private set; } = [];
    
    public static AboutUsEntity Create(
        Guid id,
        AboutUsDto model
    )
    {
        var entity = new AboutUsEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(AboutUsDto model)
    {
        Title = model.Title;
        Description = model.Description;
        ImageId = model.ImageId;

        // Update Items
        Items.Clear();
        Items.AddRange(model.Items.Select(item => AboutUsItemEntity.Create(Id, item)));

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}
