using lrembecki.core.storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapStorage
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }

    public static WebApplication MapStorage(this WebApplication app)
    {
        var group = app.MapGroup("/api/storage")
            .WithTags("Storage")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapGet("/", (
            [FromServices] IStorageService storageService,
            CancellationToken ct
        ) => storageService.GetAllAsync(ct))
            .WithTags("Get");

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromStorage(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapStorage).Assembly);
        return modelBuilder;
    }
}
