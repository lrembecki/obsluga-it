using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.IndividualTrips;

internal class IndividualTripItemEntity
{
    public Guid IndividualTripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Uom { get; private set; } = string.Empty;

    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;

    public static IndividualTripItemEntity Create(
        Guid individualTripId,
        IndividualTripItemDto model
    )
    {
        var entity = new IndividualTripItemEntity
        {
            IndividualTripId = individualTripId,
            Order = model.Order
        };
        entity.Update(model);
        return entity;
    }

    public void Update(IndividualTripItemDto model)
    {
        Title = model.Title;
        Description = model.Description;
        Price = model.Price;
        Uom = model.Uom;
        ImageId = model.ImageId!.Value;
    }
}
