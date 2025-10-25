namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record UserDto(string Email, List<Guid> SubscriptionIds);
