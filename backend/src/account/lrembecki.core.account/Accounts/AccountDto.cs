namespace lrembecki.core.account.Accounts;

public record AccountDto
{
    public string Email { get; init; } = string.Empty;
    public ICollection<Guid> PermissionGroups { get; init; } = [];
}
