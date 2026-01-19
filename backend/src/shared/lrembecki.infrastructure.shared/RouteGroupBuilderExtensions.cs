using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.infrastructure.shared;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder MapGetRequest(
        this RouteGroupBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapGet(route, delegateHandler);

        endpoint = MapAuthorization(roles, endpoint);

        return group;
    }

    public static RouteGroupBuilder MapPutRequest(
        this RouteGroupBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapPut(route, delegateHandler);

        endpoint = MapAuthorization(roles, endpoint);

        return group;
    }

    private static RouteHandlerBuilder MapAuthorization(string[] roles, RouteHandlerBuilder endpoint)
    {
        if (roles?.Length > 0)
        {
            endpoint = endpoint
                .RequireAuthorization(e =>
                {
                    foreach (var role in roles)
                    {
                        e.RequireRole(role);
                    }
                });
        }

        return endpoint;
    }
}
