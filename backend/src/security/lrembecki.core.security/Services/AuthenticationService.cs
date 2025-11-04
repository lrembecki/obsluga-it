using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Helpers;
using lrembecki.core.security.ViewModels;
using lrembecki.core.Services;
using lrembecki.core.subscription.Entities;
using lrembecki.core.subscription.ViewModels;

namespace lrembecki.core.security.Services;

public interface IAuthenticationService
{
    Task<AuthenticationViewModel> AuthenticateAsync(string email, Guid? subscriptionId, CancellationToken ct);
}

internal class AuthenticationService(IUnitOfWork uow, IJwtTokenFactory jwtTokenFactory, IDateProvider date) : IAuthenticationService
{
    public Task<AuthenticationViewModel> AuthenticateAsync(string email, Guid? subscriptionId, CancellationToken ct)
    {
        var accountSubscriptionsQuery = uow.GetRepository<AccountSubscriptionEntity>().GetAll(e => e.IsActive);
        var subscriptionsQuery = uow.GetRepository<SubscriptionEntity>().GetAll();
        var accounts = uow.GetRepository<AccountEntity>().GetAll(e => e.Email.ToUpper() == email.ToUpper());

        var now = date.UtcNow;
        var query = accountSubscriptionsQuery
            .Join(
                inner: accounts,
                outerKeySelector: l => l.AccountId,
                innerKeySelector: a => a.Id,
                resultSelector: (outer, inner) => new { AccountSubscription = outer, Account = inner })
            .Join(
                inner: subscriptionsQuery,
                outerKeySelector: l => l.AccountSubscription.SubscriptionId,
                innerKeySelector: a => a.Id,
                resultSelector: (outer, inner) => new { outer.AccountSubscription, outer.Account, Subscription = inner });

        var list = query
            .Select(e => new
            {
                AccountSubscription = AccountSubscriptionVM.Map(e.AccountSubscription),
                Subscription = SubscriptionVM.Map(e.Subscription),
                Account = AccountVM.Map(e.Account)
            }).ToList();

        var subscriptions = list.Select(e => e.Subscription).ToList();

        var data = list.SingleOrDefault(e => (subscriptionId == null && e.AccountSubscription.IsDefault) || e.AccountSubscription.SubscriptionId == subscriptionId)
            ?? throw new ArgumentNullException(nameof(email), "You are not authenticated to access this platform");

        var permissions = data.AccountSubscription.PermissionGroups
                .SelectMany(pg => pg.Permissions)
                .Select(p => p.Name)
                .Distinct()
                .ToArray();

        return Task.FromResult(
            new AuthenticationViewModel(
                IsAuthenticated: true,
                User: data.Account,
                Subscription: data.Subscription,
                Subscriptions: subscriptions,
                Permissions: permissions,
                Created: now,
                Expires: now.AddDays(15),
                Token: jwtTokenFactory.CreateToken(
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
