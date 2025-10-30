using lrembecki.shared.domain.Entities;

namespace lrembecki.obsluga_it.domain.Entities;

public class TripImageEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public ImageBlobEntity Image { get; private set; } = default!;

    public bool IsMain { get; private set; }
    public int Order { get; private set; }

    public static TripImageEntity Create(bool isMain, int order, ImageBlobEntity image)
    {
        var entity = new TripImageEntity();

        entity.Update(isMain, order, image);

        return entity;
    }

    public void Update(bool isMain, int order, ImageBlobEntity image)
    {
        IsMain = isMain;
        Order = order;
        Image = image;
    }
}
