using lrembecki.core.storage.Entities;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public record TripAgendaVM(
    int Order,
    string Content
)
{
    public static TripAgendaVM Map(TripAgendaEntity entity)
        => new TripAgendaVM(entity.Order, entity.Content);
}

public record TripAgendaDto
{
    public int Order { get; init; }
    public string Content { get; init; } = string.Empty;
}

public class TripAgendaEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Content { get; private set; } = string.Empty;

    public static TripAgendaEntity Create(Guid tripId, TripAgendaDto model)
    {
        var entity = new TripAgendaEntity { TripId = tripId };

        entity.Update(model);

        return entity;
    }

    public void Update(TripAgendaDto model)
    {
        Order = model.Order;
        Content = model.Content;
    }
}

public class TripImageEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = default!;
    public int Order { get; private set; }

    public static TripImageEntity Create(Guid tripId, TripImageDto model)
    {
        var entity = new TripImageEntity()
        {
            TripId = tripId,
            ImageId = model.ImageId!.Value
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripImageDto model)
    {
        Order = model.Order;
    }
}
