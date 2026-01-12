using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;

using lrembecki.core.GlobalFilters;
using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.infrastructure.Extensions;
using lrembecki.infrastructure.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace lrembecki.infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder, bool isDevelopment, string appConfiguration, string tenantId)
    {
        builder.AddAzureAppConfiguration(isDevelopment, appConfiguration, tenantId);
        builder.AddProjectAuthentication();

        builder.Services.AddAccount();
        builder.Services.AddForms();
        builder.Services.AddSecurity();
        builder.Services.AddStorage();
        builder.Services.AddSettings();
        builder.Services.AddTrotamundos();
        // Add service registrations from other modules here

        builder.Services.AddScoped<ObslugaItDbContext>();
        builder.Services.AddDbContext<ObslugaItDbContext>(_ => _.UseSqlServer(
            builder.Configuration.GetConnectionString("Sql"), 
            s => s.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        );

        builder.Services.AddScoped((_) => new BlobServiceClient(builder.Configuration.GetConnectionString("Blob")!));
        builder.Services.AddScoped((_) => new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus")!));

        builder.Services.AddHttpContextAccessor();

        var cors = builder.Configuration["Cors"]!.Split(Environment.NewLine);
        builder.Services
            .AddCors(_ => _
                .AddDefaultPolicy(p => p.WithOrigins(cors)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            )
            .AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

        builder.Services.AddScoped<ISender, Sender>();
        builder.Services.AddSingleton<IJwtTokenFactory, JwtTokenFactory>();
        builder.Services.AddScoped<ISessionAccessor, SessionAccessor>();
        builder.Services.AddScoped<IDateProvider, DateProvider>();
        builder.Services.AddScoped<IBlobHelper, BlobHelper>();
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped<INotifier, Notifier>();

        builder.Services.AddScoped<SubscriptionIdGlobalFilter>();
        builder.Services.AddScoped<ICollection<IGlobalFilter>>(provider => [
            provider.GetRequiredService<SubscriptionIdGlobalFilter>()
        ]);
    }
}
