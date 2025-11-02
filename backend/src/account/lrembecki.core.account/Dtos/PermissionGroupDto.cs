namespace lrembecki.core.account.Dtos;

public record PermissionGroupDto
{
    public string Name { get; init; } = string.Empty;
    public List<Guid> Permissions { get; init; } = [];
}
