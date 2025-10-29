namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripScheduleDto
{
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
