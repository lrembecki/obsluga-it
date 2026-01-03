namespace lrembecki.core.trotamundos.Highlights;

public record HighlightVM(
    Guid Id,
    string Title,
    string Icon
)
{
    internal static HighlightVM Map(HighlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new HighlightVM(
            entity.Id,
            entity.Title,
            entity.Icon
        );
    }
}
