namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksDto
{
    public string Title { get; init; } = string.Empty;
    public string HeaderText { get; init; } = string.Empty;
    public string FooterText { get; init; } = string.Empty;
    public List<HowItWorksItemDto> Items { get; init; } = [];
}
