using lrembecki.core.storage.Services;
using lrembecki.presentation.account;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.core.storage;

public static class ServiceRegistration
{
    public static void AddStorage(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStorageService, StorageService>();
    }

    public static void MapStorage(this WebApplication app)
    {

        var group = app.MapGroup("/api/storage")
            .WithTags("Storage")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapStorage();
    }
}
