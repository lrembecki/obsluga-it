namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripPriceIncludeDto
{
    public int Order { get; set; }
    public bool Includes { get; set; }
    public string Content { get; set; } = string.Empty;
}
