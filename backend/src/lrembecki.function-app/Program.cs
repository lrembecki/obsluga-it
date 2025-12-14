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
using lrembecki.function_app.Helpers;
using lrembecki.presentation.forms;

var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.AddStorage();
builder.AddTrotamundos();
builder.AddSettings();
builder.AddForms();
builder.AddInfrastructure(
    isDevelopment: Environment.GetEnvironmentVariable("IsDevelopment") == "true",
    appConfiguration: Environment.GetEnvironmentVariable("AppConfiguration")!, 
    tenantId: Environment.GetEnvironmentVariable("TenantId")!);

var subscriptionId = Guid.Parse(Environment.GetEnvironmentVariable("SubscriptionId")!);

builder.Services.AddScoped(_ => new PredefinedSessionAccessor(subscriptionId));
builder.Services.AddScoped<ISessionAccessor>(_ => _.GetRequiredService<PredefinedSessionAccessor>());
builder.Services.AddScoped<UploadHelper>();
builder.Services.AddScoped<lrembecki.core.Services.IPublisher, PublishFactory>();
builder.Services.AddScoped<TripPublisher>();
builder.Services.AddScoped<ContactPublisher>();
builder.Services.AddScoped<FilePublisher>();
builder.Services.AddScoped<FormDefinitionPublisher>();

await builder.Build().RunAsync();
