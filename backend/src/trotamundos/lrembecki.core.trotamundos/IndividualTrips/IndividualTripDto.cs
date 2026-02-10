using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripDto : IHasStorageCollection<IndividualTripDto, IndividualTripItemDto>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<IndividualTripItemDto> Items { get; init; } = [];
    public List<IndividualTripStepItemDto> Steps { get; init; } = [];

    public List<IndividualTripItemDto> GetStoragecollection() 
        => Items;

    public IndividualTripDto UpdateStorageCollection(List<IndividualTripItemDto> items)
        => this with { Items = items };
}
