using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Events;

namespace lrembecki.obsluga_it.domain.Entities;

internal class UserEntity : BaseEntity, IHasId<Guid>
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
