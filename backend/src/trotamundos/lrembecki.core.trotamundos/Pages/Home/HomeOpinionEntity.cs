using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.Home;

internal class HomeOpinionEntity
{
    public Guid HomeId { get; private set; }
    public int Order { get; private set; }

    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;

    public static HomeOpinionEntity Create(
        Guid homeId,
        HomeOpinionDto model
    )
    {
        var entity = new HomeOpinionEntity
        {
            HomeId = homeId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(HomeOpinionDto model)
    {
        Order = model.Order;
        ImageId = model.ImageId ?? Guid.Empty;
    }
}
