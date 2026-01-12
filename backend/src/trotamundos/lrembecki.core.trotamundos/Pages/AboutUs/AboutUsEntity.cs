using lrembecki.core.Events;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

internal class AboutUsPersonEntity
{
    public Guid AboutUsId { get; private set; }
    public int Order { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;

    public static AboutUsPersonEntity Create(
        Guid aboutUsId,
        AboutUsPersonDto model
    )
    {
        var entity = new AboutUsPersonEntity
        {
            AboutUsId = aboutUsId
        };
        entity.Update(model);
        return entity;
    }

    public void Update(AboutUsPersonDto model)
    {
        Order = model.Order;
        Name = model.Name;
        Description = model.Description;
        ImageId = model.ImageId;
    }
}

public record AboutUsPersonDto
{
    public int Order { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
}

public record AboutUsPersonVM(
    int Order,
    string Name,
    string Description,
    Guid ImageId,
    StorageVM Image
) 
{ 
    internal static AboutUsPersonVM Map(AboutUsPersonEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.Order,
            entity.Name,
            entity.Description,
            entity.ImageId,
            StorageVM.Map(entity.Image)
        );
    }
}

internal class AboutUsEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;
    public List<AboutUsItemEntity> Items { get; private set; } = [];
    public List<AboutUsPersonEntity> Persons { get; private set; } = [];

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

        // Update Persons
        Persons.Clear();
        Persons.AddRange(model.Persons.Select(person => AboutUsPersonEntity.Create(Id, person)));

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}
