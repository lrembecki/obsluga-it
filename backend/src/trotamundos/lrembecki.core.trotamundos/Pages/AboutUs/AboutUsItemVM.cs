namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsItemVM(
    int Order,
    string Icon,
    string Title,
    string Description
)
{ 
    internal static AboutUsItemVM Map(AboutUsItemEntity entity)
    {
        if (entity == null) return null!;
        return new(
            entity.Order,
            entity.Icon,
            entity.Title,
            entity.Description
        );
    }
}