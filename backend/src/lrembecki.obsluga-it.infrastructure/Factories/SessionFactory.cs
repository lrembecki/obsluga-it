using lrembecki.obsluga_it.application.Abstractions.Factories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace lrembecki.obsluga_it.infrastructure.Factories;

internal class SessionFactory(IHttpContextAccessor accessor) : ISessionFactory
{
    private readonly ClaimsPrincipal? _user = accessor.HttpContext?.User;

    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated == true;

    public string? Email => _user?.Claims
        .FirstOrDefault(c => c.Type is "emails" or "preferred_username" or ClaimTypes.Email)?.Value;

    public Guid? SelectedSubscriptionId
        => Guid.TryParse(_user?.Claims.FirstOrDefault(c => c.Type == "subscriptionId")?.Value, out var id)
            ? id
            : null;
}
