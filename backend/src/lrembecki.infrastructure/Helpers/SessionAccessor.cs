using lrembecki.core.Helpers;
using lrembecki.infrastructure.Defaults;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace lrembecki.infrastructure.Helpers;

internal class SessionAccessor(IHttpContextAccessor accessor) : ISessionAccessor
{
    private readonly ClaimsPrincipal? _user = accessor.HttpContext?.User;

    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated == true;

    public string? Email => _user?.Claims
        .FirstOrDefault(c => c.Type is CustomClaimTypes.Email)?.Value;

    public Guid? SubscriptionId
    {
        get
        {
            var subscriptionIdRaw = _user?.Claims.FirstOrDefault(c => c.Type is CustomClaimTypes.SubscriptionId)?.Value;
            return Guid.TryParse(subscriptionIdRaw, out var subscriptionId) ? subscriptionId : null;
        }
    }

    public Guid? UserId
    {
        get
        {
            var userIdRaw = _user?.Claims.FirstOrDefault(c => c.Type is CustomClaimTypes.UserId)?.Value;
            return Guid.TryParse(userIdRaw, out var userId) ? userId : null;
        }
    }
}
