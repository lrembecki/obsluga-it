namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record SubscriptionUserDto
{
    public Guid UserId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;
}