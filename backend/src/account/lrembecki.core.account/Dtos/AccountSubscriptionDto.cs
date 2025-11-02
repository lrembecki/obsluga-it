namespace lrembecki.core.account.Dtos;

public record AccountSubscriptionDto
{
    public bool IsActive { get; init; }
    public bool IsDefault { get; init; }
    public ICollection<Guid> PermissionGroups { get; init; } = [];
}
