using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripPriceIncludeVM(
    Guid TripId,
    int Order,
    bool Includes,
    string Content
)
{
    public static TripPriceIncludeVM Map(TripPriceIncludeEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripPriceIncludeVM(
            entity.TripId,
            entity.Order,
            entity.Includes,
            entity.Content
        );
    }
}

