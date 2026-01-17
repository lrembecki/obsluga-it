using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksDto : IHasStorageCollection<HowItWorksDto, HowItWorksItemDto>
{
    public string Title { get; init; } = string.Empty;
    public string HeaderText { get; init; } = string.Empty;
    public string FooterText { get; init; } = string.Empty;
    public List<HowItWorksItemDto> Items { get; init; } = [];

    public List<HowItWorksItemDto> GetStoragecollection()
        => Items;

    public HowItWorksDto UpdateStorageCollection(List<HowItWorksItemDto> items)
        => this with { Items = items };
}
