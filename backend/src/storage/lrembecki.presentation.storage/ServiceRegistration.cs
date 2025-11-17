using lrembecki.core.storage.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lrembecki.presentation.storage;

public static class ServiceRegistration
{
    public static void AddStorage(this IHostApplicationBuilder builder)
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
