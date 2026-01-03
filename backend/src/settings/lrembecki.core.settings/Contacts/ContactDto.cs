namespace lrembecki.core.settings.Contacts;

public record ContactDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Position { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
