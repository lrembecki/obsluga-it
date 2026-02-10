using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsVM(
    string Title,
    string Description,
    string FooterDescription,
    string FooterHighlight,
    Guid ImageId,
    StorageVM Image,
    List<AboutUsItemVM> Items,
    List<AboutUsPersonVM> Persons
)
{
    internal static AboutUsVM Map(AboutUsEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.Title,
            entity.Description,
            entity.FooterDescription,
            entity.FooterHighlight,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            [.. entity.Items.Select(AboutUsItemVM.Map)],
            [.. entity.Persons.Select(AboutUsPersonVM.Map)]
        );
    }

    public static AboutUsVM Empty => new(
        Title: string.Empty,
        Description: string.Empty,
        FooterDescription: string.Empty,
        FooterHighlight: string.Empty,
        ImageId: new Guid(),
        Image: null!,
        Items: [],
        Persons: []
    );

}
