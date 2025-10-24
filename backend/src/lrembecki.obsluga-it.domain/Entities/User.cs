using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.domain.Entities;

public class User : BaseEntity, IHasId<Guid>
{
    private HashSet<UserSubscription> _userSubscriptions = [];
    public IReadOnlyCollection<UserSubscription> UserSubscriptions => _userSubscriptions.AsReadOnly();

    public Guid Id { get; set; }
    public Email Email { get; set; } = default!;

    public static User Create(Guid id, Email email)
    {
        var user = new User
        {
            Id = id,
            Email = email
        };

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email));

        return user;
    }

    public record UserCreatedDomainEvent(Guid UserId, Email Email) : DomainEvent(Guid.NewGuid(), nameof(User), DateTime.UtcNow);
}
