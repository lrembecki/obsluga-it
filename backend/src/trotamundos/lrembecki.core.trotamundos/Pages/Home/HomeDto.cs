using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.Home;

public record HomeDto : IHasStorageCollection<HomeDto, HomeOpinionDto>
{
    public Guid BackgroundVideoId { get; init; }
    public StorageDto BackgroundVideo { get; init; } = null!;

    public Guid TrailerVideoId { get; init; }
    public StorageDto TrailerVideo { get; init; } = null!;

    public List<HomeOpinionDto> Opinions { get; init; } = [];

    public List<HomeOpinionDto> GetStoragecollection()
        => new([.. Opinions]);

    public HomeDto UpdateStorageCollection(List<HomeOpinionDto> items)
        => this with { Opinions = items };
}
