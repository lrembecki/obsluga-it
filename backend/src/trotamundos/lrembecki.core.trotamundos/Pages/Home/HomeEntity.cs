using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Pages.Home;

internal class HomeEntity : TrotamundosBaseEntity
{
    public Guid BackgroundVideoId { get; private set; }
    public StorageEntity BackgroundVideo { get; private set; } = null!;

    public Guid TrailerVideoId { get; private set; }
    public StorageEntity TrailerVideo { get; private set; } = null!;

    public List<HomeOpinionEntity> Opinions { get; private set; } = [];
}

internal class HomeOpinionEntity
{
    public Guid HomeId { get; private set; }
    public int Order { get; private set; }

    public StorageEntity Storage { get; private set; } = null!;
}

public record HomeOpinionDto
{
    public Guid HomeId { get; init; }
    public int Order { get; init; }
}
