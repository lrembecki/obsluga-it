using lrembecki.core.storage;

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
            AboutUsId = aboutUsId,
            Order = model.Order
        };
        entity.Update(model);
        return entity;
    }

    public void Update(AboutUsPersonDto model)
    {
        Name = model.Name;
        Description = model.Description;
        ImageId = model.ImageId!.Value;
    }
}
