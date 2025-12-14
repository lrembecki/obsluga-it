using lrembecki.core.Helpers;
using lrembecki.infrastructure;
using lrembecki.infrastructure.Helpers;
using lrembecki.presentation.forms;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();


builder.AddForms();
builder.AddInfrastructure(
    isDevelopment: Environment.GetEnvironmentVariable("IsDevelopment") == "true",
    appConfiguration: Environment.GetEnvironmentVariable("AppConfiguration")!,
    tenantId: Environment.GetEnvironmentVariable("TenantId")!);

var subscriptionId = Guid.Parse(Environment.GetEnvironmentVariable("SubscriptionId")!);
builder.Services.AddScoped<ISessionAccessor>(_ => new PredefinedSessionAccessor(subscriptionId));

builder.Build().Run();
