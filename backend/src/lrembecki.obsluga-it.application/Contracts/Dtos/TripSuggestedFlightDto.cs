using lrembecki.shared.application.Dtos;

namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripSuggestedFlightDto
{
    public int Order { get; set; }
    public ImageBlobDto Image { get; set; } = default!;
}
