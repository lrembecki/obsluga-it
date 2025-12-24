using lrembecki.core.storage.Dtos;
using System.Security.Cryptography.X509Certificates;

namespace lrembecki.core.trotamundos.Dtos;

public record AboutUsDto
{
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
