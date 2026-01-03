using lrembecki.core.account.Accounts;
using lrembecki.core.account.AccountSubscriptions;
using lrembecki.core.account.PermissionGroups;
using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.security;

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

        var pIds = data.AccountSubscription.PermissionGroups.ToList();
        var permissions = uow.GetRepository<PermissionGroupEntity>()
            .GetAll(e => pIds.Contains(e.Id))
            .SelectMany(e => e.Permissions)
            .Select(e => e.Name)
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
