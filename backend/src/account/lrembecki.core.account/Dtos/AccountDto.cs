namespace lrembecki.core.account.Dtos;

public record AccountDto
{
    public string Email { get; init; } = string.Empty;
    public ICollection<Guid> PermissionGroups { get; init; } = [];
}
