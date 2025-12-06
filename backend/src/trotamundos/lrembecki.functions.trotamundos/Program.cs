using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using lrembecki.presentation.storage;
using lrembecki.presentation.trotamundos;
using lrembecki.presentation.settings;
using lrembecki.infrastructure;
using lrembecki.core.Helpers;
using lrembecki.infrastructure.Helpers;

var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.AddStorage();
builder.AddTrotamundos();
builder.AddSettings();
builder.AddInfrastructure(
    isDevelopment: Environment.GetEnvironmentVariable("IsDevelopment") == "true",
    appConfiguration: Environment.GetEnvironmentVariable("AppConfiguration")!, 
    tenantId: Environment.GetEnvironmentVariable("TenantId")!);

var subscriptionId = Guid.Parse(Environment.GetEnvironmentVariable("SubscriptionId")!);

builder.Services.AddScoped<ISessionAccessor>(_ => new PredefinedSessionAccessor(subscriptionId));

await builder.Build().RunAsync();
