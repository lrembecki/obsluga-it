using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripHighlightVM(
    int Order,
    Guid HighlightId,
    HighlightVM Highlight,
    string Value
)
{
    public static TripHighlightVM Map(TripHighlightEntity entity)
    {
        if (entity == null) return null!;

        return new TripHighlightVM(
            entity.Order,
            entity.HighlightId,
            HighlightVM.Map(entity.Highlight),
            entity.Value
        );
    }
}