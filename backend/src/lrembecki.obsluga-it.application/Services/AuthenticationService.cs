using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;
using lrembecki.shared.application.ViewModels;

namespace lrembecki.obsluga_it.application.Services;

public interface IAuthenticationService
{
    Task<AccountVM> SignInAsync(string email, Guid? subscriptionId, CancellationToken cancellationToken = default);
}

internal class AuthenticationService(
    IDateProvider dateProvider,
    IJwtTokenFactory tokenFactory,
    IUnitOfWork uow) : IAuthenticationService
{
    public async Task<AccountVM> SignInAsync(string email, Guid? subscriptionId, CancellationToken cancellationToken = default)
    {
        var now = dateProvider.UtcNow;

        var subscriptionUser = await uow.GetRepository<SubscriptionUserEntity>()
            .FirstOrDefaultAsync(e => e.User.Email == email)
            ?? throw new UnauthorizedAccessException("Invalid email or subscription.");

        var userVM = UserVM.MapFromDomainEntity(subscriptionUser.User);
        var subscriptionVM = SubscriptionVM.MapFromDomainEntity(subscriptionUser.Subscription);

        var permissions = Enumerable.Empty<string>();

        var token = tokenFactory.CreateToken(userVM, permissions, subscriptionVM, now, now.AddDays(15));

        return new AccountVM(
            User: userVM,
            Subscription: subscriptionVM,
            Permissions: [.. permissions],
            Token: token,
            Created: now,
            Expires: now.AddDays(15),
            IsAuthenticated: true);
    }
}
