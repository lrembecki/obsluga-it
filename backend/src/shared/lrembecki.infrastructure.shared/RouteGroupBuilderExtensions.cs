using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.infrastructure.shared;

public static class RouteGroupBuilderExtensions
{
    public static IEndpointRouteBuilder MapGetRequest(
        this IEndpointRouteBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapGet(route, delegateHandler)
            .MapAuthorization(roles);

        return group;
    }
    public static IEndpointRouteBuilder MapDeleteRequest(
        this IEndpointRouteBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapDelete(route, delegateHandler)
            .MapAuthorization(roles);

        return group;
    }

    public static IEndpointRouteBuilder MapPutRequest(
        this IEndpointRouteBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapPut(route, delegateHandler)
            .MapAuthorization(roles);

        return group;
    }

    public static IEndpointRouteBuilder MapPostRequest(
        this IEndpointRouteBuilder group,
        Delegate delegateHandler,
        string route = "",
        string[] roles = null!)
    {
        var endpoint = group
            .MapPost(route, delegateHandler)
            .MapAuthorization(roles);

        return group;
    }

    private static RouteHandlerBuilder MapAuthorization(this RouteHandlerBuilder endpoint, string[] roles)
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
