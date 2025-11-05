namespace lrembecki.core.account.Dtos;

public record AccountSubscriptionDto
{
    public Guid? AccountId { get; init; }
    public bool IsActive { get; init; }
    public bool IsDefault { get; init; }
    public ICollection<Guid> PermissionGroups { get; init; } = [];
}
