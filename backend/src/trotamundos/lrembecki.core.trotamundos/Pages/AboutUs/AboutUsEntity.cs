using lrembecki.core.Events;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

internal class AboutUsEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string FooterDescription { get; private set; } = string.Empty;
    public string FooterHighlight { get; private set; } = string.Empty;
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
        FooterDescription = model.FooterDescription;
        FooterHighlight = model.FooterHighlight;
        ImageId = model.ImageId;

        // Update Items
        Items.Clear();
        Items.AddRange(model.Items.Select(item => AboutUsItemEntity.Create(Id, item)));

        // Update Persons
        var toDelete = Persons.Where(e => !model.Persons.Select(y => y.Order).Contains(e.Order)).ToList();
        var toUpdate = Persons.Where(e => model.Persons.Select(y => y.Order).Contains(e.Order)).ToList();
        var toCreate = model.Persons.Where(e => !Persons.Select(y => y.Order).Contains(e.Order)).ToList();

        foreach (var item in toUpdate) item.Update(model.Persons.Find(y => y.Order == item.Order)!);
        foreach (var item in toDelete) Persons.Remove(item);
        foreach (var item in toCreate) Persons.Add(AboutUsPersonEntity.Create(Id, item));

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}
