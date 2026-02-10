namespace lrembecki.core.trotamundos.IndividualTrips;

internal class IndividualTripStepItemEntity
{
    public Guid IndividualTripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static IndividualTripStepItemEntity Create(
        Guid individualTripId,
        IndividualTripStepItemDto model
    )
    {
        var entity = new IndividualTripStepItemEntity
        {
            IndividualTripId = individualTripId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(IndividualTripStepItemDto model)
    {
        Order = model.Order;
        Title = model.Title;
        Description = model.Description;
    }
}
