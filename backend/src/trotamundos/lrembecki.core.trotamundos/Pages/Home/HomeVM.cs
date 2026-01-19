using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.Home;

public record HomeVM(
    Guid BackgroundVideoId,
    StorageVM BackgroundVideo,
    Guid TrailerVideoId,
    StorageVM TrailerVideo,
    List<HomeOpinionVM> Opinions
)
{
    internal static HomeVM Map(HomeEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.BackgroundVideoId,
            StorageVM.Map(entity.BackgroundVideo),
            entity.TrailerVideoId,
            StorageVM.Map(entity.TrailerVideo),
            [.. entity.Opinions.OrderBy(x => x.Order).Select(HomeOpinionVM.Map)]
        );
    }

    public static HomeVM Empty => new(
        Guid.Empty,
        null!,
        Guid.Empty,
        null!,
        []
    );
}
