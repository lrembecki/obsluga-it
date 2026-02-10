using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripItemVM(
    int Order,
    string Title,
    string Description,
    decimal Price,
    string Uom,
    Guid ImageId,
    StorageVM Image
)
{
    internal static IndividualTripItemVM Map(IndividualTripItemEntity entity)
    {
        if (entity == null) return null!;
        return new(
            entity.Order,
            entity.Title,
            entity.Description,
            entity.Price,
            entity.Uom,
            entity.ImageId,
            StorageVM.Map(entity.Image)
        );
    }
}
