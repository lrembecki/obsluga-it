using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Events;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.domain.Entities;

public class UserEntity : BaseEntity, IHasId<Guid>
{
    private HashSet<UserSubscriptionEntity> _userSubscriptions = [];
    public IReadOnlyCollection<UserSubscriptionEntity> UserSubscriptions => _userSubscriptions.ToList().AsReadOnly();

    public Guid Id { get; set; }
    public Email Email { get; set; } = default!;

    public static UserEntity Create(Guid id, Email email)
    {
        var user = new UserEntity
        {
            Id = id,
            Email = email
        };

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email));

        return user;
    }
}
