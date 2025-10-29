namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record LoyalityProgramDto
{
    public ImageBlobDto Image { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}