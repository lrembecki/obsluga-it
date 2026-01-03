namespace lrembecki.core.trotamundos.Trips;

public record TripScheduleDto
{
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
