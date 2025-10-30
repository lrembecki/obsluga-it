using lrembecki.obsluga_it.domain.Events;
using lrembecki.shared.domain.Abstractions;
using lrembecki.shared.domain.Entities;

namespace lrembecki.obsluga_it.domain.Entities;

public class UserEntity : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<SubscriptionUserEntity> _userSubscriptions = [];
    public IReadOnlyCollection<SubscriptionUserEntity> UserSubscriptions => _userSubscriptions.ToList().AsReadOnly();

    public Guid Id { get; set; }
    public string Email { get; set; } = default!;

    public static UserEntity Create(Guid id, string email)
    {
        var user = new UserEntity
        {
            Id = id,
            Email = email
        };

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Email));

        return user;
    }

    public void AddSubscription(SubscriptionUserEntity userSubscription)
    {
        this._userSubscriptions.Add(userSubscription);
    }
}
