using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Advantages;

internal class AdvantageEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public static AdvantageEntity Create(Guid id, AdvantageDto model)
        => new AdvantageEntity
        {
            Id = id,
            Title = model.Title,
            Content = model.Content
        };

    public void Update(AdvantageDto model)
    {
        Title = model.Title;
        Content = model.Content;
    }
}
