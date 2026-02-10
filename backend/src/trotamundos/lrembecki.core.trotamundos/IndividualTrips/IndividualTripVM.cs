namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripVM(
    string Title,
    string Description,
    List<IndividualTripItemVM> Items,
    List<IndividualTripStepItemVM> Steps
)
{
    internal static IndividualTripVM Map(IndividualTripEntity entity)
    {
        if (entity == null) return null!;
        return new(
            entity.Title,
            entity.Description,
            [.. entity.Items.Select(IndividualTripItemVM.Map)],
            [.. entity.Steps.Select(IndividualTripStepItemVM.Map)]
        );
    }

    public static IndividualTripVM Empty()
        => new(string.Empty, string.Empty, [], []);
}
