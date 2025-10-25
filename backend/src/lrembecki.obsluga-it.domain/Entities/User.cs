using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;
using lrembecki.obsluga_it.domain.Events;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.domain.Entities;

public class User : BaseEntity, IHasId<Guid>
{
    private HashSet<SubscriptionUser> _userSubscriptions = [];
    public IReadOnlyCollection<SubscriptionUser> UserSubscriptions => _userSubscriptions.ToList().AsReadOnly();

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
}
