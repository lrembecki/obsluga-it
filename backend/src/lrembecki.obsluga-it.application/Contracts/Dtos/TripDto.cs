namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripDto
{
 public string Title { get; set; } = string.Empty;
 public string Subtitle { get; set; } = string.Empty;
 public string Description { get; set; } = string.Empty;
}
