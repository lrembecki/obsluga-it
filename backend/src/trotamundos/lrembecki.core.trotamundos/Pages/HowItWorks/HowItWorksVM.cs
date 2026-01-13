namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksVM(
    string Title,
    string HeaderText,
    string FooterText,
    List<HowItWorksItemVM> Items
)
{
    internal static HowItWorksVM Map(HowItWorksEntity entity)
    {
        if (entity == null) return null!;
        return new(
            entity.Title,
            entity.HeaderText,
            entity.FooterText,
            [.. entity.Items.OrderBy(x => x.Order).Select(HowItWorksItemVM.Map)!]
        );
    }

    public static HowItWorksVM Empty => new(
        string.Empty,
        string.Empty,
        string.Empty,
        []
    );
}
