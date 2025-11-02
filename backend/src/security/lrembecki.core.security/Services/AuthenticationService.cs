using lrembecki.core.account.Entities;
using lrembecki.core.Helpers;
using lrembecki.core.security.ViewModels;
using lrembecki.core.Services;
using lrembecki.core.subscription.Entities;

namespace lrembecki.core.security.Services;

public interface IAuthenticationService
{
    Task<AuthenticationViewModel> AuthenticateAsync(string email, Guid? subscriptionId, CancellationToken ct);
}

internal class AuthenticationService(IUnitOfWork uow, IJwtTokenFactory jwtTokenFactory, IDateProvider date) : IAuthenticationService
{
    public Task<AuthenticationViewModel> AuthenticateAsync(string email, Guid? subscriptionId, CancellationToken ct)
    {
        var accountSubscriptionsQuery = uow.GetRepository<AccountSubscriptionEntity>().GetAll();
        var subscriptions = uow.GetRepository<SubscriptionEntity>().GetAll(e => subscriptionId == null || subscriptionId == e.Id);
        var accounts = uow.GetRepository<AccountEntity>().GetAll(e => e.Email == email);

        var now = date.UtcNow;
        var query = accountSubscriptionsQuery
            .Join(
                inner: accounts,
                outerKeySelector: l => l.AccountId,
                innerKeySelector: a => a.Id,
                resultSelector: (outer, inner) => new { AccountSubscription = outer, Account = inner })
            .Join(
                inner: subscriptions,
                outerKeySelector: l => l.AccountSubscription.SubscriptionId,
                innerKeySelector: a => a.Id,
                resultSelector: (outer, inner) => new { outer.AccountSubscription, outer.Account, Subscription = inner });

        var data = query.SingleOrDefault()
        ?? throw new ArgumentNullException(nameof(email), "You are not authenticated to access this platform");

        var permissions = data.AccountSubscription.PermissionGroups
                .SelectMany(pg => pg.Permissions)
                .Select(p => p.Name)
                .Distinct()
                .ToArray();

        return Task.FromResult(
            new AuthenticationViewModel(
                UserId: data.Account.Id,
                SubscriptionId: data.Subscription.Id,
                Permissions: permissions,
                jwtToken: jwtTokenFactory.CreateToken(
                    identity: jwtTokenFactory.GetClaimsIdentity(
                        subscriptionId: data.Subscription.Id, 
                        userId: data.Account.Id, 
                        email: data.Account.Email,
                        permissions: permissions, 
                        created: now, 
                        expires: now.AddDays(15)
                    ),
                    permissionVMs: permissions,
                    created: now,
                    expires: now.AddDays(15)
                )
            )
        );
    }
}
