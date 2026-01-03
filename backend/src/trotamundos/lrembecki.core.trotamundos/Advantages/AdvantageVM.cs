namespace lrembecki.core.trotamundos.Advantages;

public record AdvantageVM(
    Guid Id,
    string Title,
    string Content
)
{
    internal static AdvantageVM Map(AdvantageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new AdvantageVM(
            entity.Id,
            entity.Title,
            entity.Content
        );
    }
}
