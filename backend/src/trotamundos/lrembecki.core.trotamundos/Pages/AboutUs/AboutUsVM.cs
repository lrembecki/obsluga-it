using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsVM(
    string Title,
    string Description,
    Guid ImageId,
    StorageVM Image,
    List<AboutUsItemVM> Items
)
{
    internal static AboutUsVM Map(AboutUsEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.Title,
            entity.Description,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            [.. entity.Items.Select(AboutUsItemVM.Map)]
        );
    }

    public static AboutUsVM Empty => new(
        string.Empty,
        string.Empty,
        new Guid(),
        null!,
        []
    );

}
