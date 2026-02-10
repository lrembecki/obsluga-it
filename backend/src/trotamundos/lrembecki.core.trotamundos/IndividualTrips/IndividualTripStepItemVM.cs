namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripStepItemVM(
    int Order,
    string Title,
    string Description
)
{
    internal static IndividualTripStepItemVM Map(IndividualTripStepItemEntity entity)
    {
        if (entity == null) return null!;
        return new(
            entity.Order,
            entity.Title,
            entity.Description
        );
    }
}
