using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Repositories;

namespace lrembecki.obsluga_it.application.Services;

internal class AuthenticationService(
    IDateProvider dateProvider, 
    IJwtTokenFactory tokenFactory,
    ISubscriptionUserRepository subscriptionUsers) : IAuthenticationService
{
    public async Task<AccountVM> SignInAsync(string email, Guid? subscriptionId, CancellationToken cancellationToken = default)
    {
        var now = dateProvider.UtcNow;

        var subscriptionUser = (await subscriptionUsers.GetByEmailAndSubscriptionId(email, subscriptionId))
            ?? throw new UnauthorizedAccessException("Invalid email or subscription.");

        var userVM = UserVM.MapFromDomainEntity(subscriptionUser.User);
        var subscriptionVM = SubscriptionVM.MapFromDomainEntity(subscriptionUser.Subscription);

        // Permissions resolution could be added here later.
        var permissions = Enumerable.Empty<string>();

        var token = tokenFactory.CreateToken(userVM, permissions, subscriptionVM, now, now.AddDays(15));

        return new AccountVM(
            User: userVM,
            Subscription: subscriptionVM,
            Permissions: permissions.ToList(),
            Token: token,
            Created: now,
            Expires: now.AddDays(15),
            IsAuthenticated: true);
    }
}
