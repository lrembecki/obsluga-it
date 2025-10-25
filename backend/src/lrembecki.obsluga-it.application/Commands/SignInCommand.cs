using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Repositories;

namespace lrembecki.obsluga_it.application.Commands;

public record SignInCommand(string Email, Guid? SubscriptionId) : IRequest<AccountVM>;

internal sealed class SignInCommandHandler(
    ITokenHelper tokenHelper,
    ISubscriptionUserRepository subscriptionUsers,
    IDateProvider date) : IRequestHandler<SignInCommand, AccountVM>
{
    public async Task<AccountVM> HandleAsync(SignInCommand request, CancellationToken cancellationToken = default)
    {
        var now = date.UtcNow;

        var subscriptionUser = (await subscriptionUsers.GetByEmailAndSubscriptionId(request.Email, request.SubscriptionId))
            ?? throw new UnauthorizedAccessException("Invalid email or subscription.");

        var userVM = UserVM.MapFromDomainEntity(subscriptionUser.User);
        var subscriptionVM = SubscriptionVM.MapFromDomainEntity(subscriptionUser.Subscription);

        var token = tokenHelper.CreateToken(userVM, [], subscriptionVM, now, now.AddDays(15));
        return new AccountVM(
                User: userVM,
                Subscription: subscriptionVM,
                Permissions: [],
                Token: token,
                Created: now,
                Expires: now.AddDays(15),
                IsAuthenticated: subscriptionUser != null);
    }
}
