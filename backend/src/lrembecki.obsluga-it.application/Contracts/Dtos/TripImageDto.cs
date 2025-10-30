using lrembecki.shared.application.Dtos;

namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripImageDto : ImageBlobDto
{
    public Guid ImageId { get; set; }
    public bool IsMain { get; set; }
    public int Order { get; set; }
}
