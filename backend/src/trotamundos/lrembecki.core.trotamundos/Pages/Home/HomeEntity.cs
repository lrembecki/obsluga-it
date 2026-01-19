using lrembecki.core.Events;
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

    public static HomeEntity Create(
        Guid id,
        HomeDto model
    )
    {
        var entity = new HomeEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(HomeDto model)
    {
        BackgroundVideoId = model.BackgroundVideoId;
        TrailerVideoId = model.TrailerVideoId;

        Opinions.Clear();
        Opinions.AddRange(model.Opinions.Select(item => HomeOpinionEntity.Create(Id, item)));

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}
