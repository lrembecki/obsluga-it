using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

internal class HowItWorksItemEntity
{
    public Guid HowItWorksId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid? ImageId { get; private set; }
    public StorageEntity? Image { get; private set; }

    public static HowItWorksItemEntity Create(
        Guid howItWorksId,
        HowItWorksItemDto model
    )
    {
        var entity = new HowItWorksItemEntity
        {
            HowItWorksId = howItWorksId,
        };

        entity.Update(model);

        return entity;
    }

    public void Update(HowItWorksItemDto model)
    {
        Order = model.Order;
        Title = model.Title;
        Description = model.Description;
        ImageId = model.ImageId;
    }
}
