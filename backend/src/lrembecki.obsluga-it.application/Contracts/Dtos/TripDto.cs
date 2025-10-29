namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<AdvantageDto> Advantages { get; set; } = [];
    public List<HighlightDto> Highlights { get; set; } = [];
    public List<TripImageDto> Images { get; set; } = [];

}
