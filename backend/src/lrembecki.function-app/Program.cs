using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using lrembecki.infrastructure;
using lrembecki.core.Helpers;
using lrembecki.infrastructure.Helpers;
using lrembecki.function_app.Helpers;

using lrembecki.function_app.Helpers.Publishers;
using lrembecki.function_app.Helpers.Notifiers;

var builder = FunctionsApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.AddInfrastructure(
    isDevelopment: Environment.GetEnvironmentVariable("IsDevelopment") == "true",
    appConfiguration: Environment.GetEnvironmentVariable("AppConfiguration")!, 
    tenantId: Environment.GetEnvironmentVariable("TenantId")!);

builder.Services.AddScoped<ISessionAccessor, PredefinedSessionAccessor>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<UploadHelper>();

builder.Services.AddScoped<lrembecki.core.Services.IEmailNotifier, NotifyFactory>();
builder.Services.AddScoped<FormNotifier>();

builder.Services.AddScoped<lrembecki.core.Services.IPublisher, PublishFactory>();
builder.Services.AddScoped<TripPublisher>();
builder.Services.AddScoped<ContactPublisher>();
builder.Services.AddScoped<FilePublisher>();
builder.Services.AddScoped<FormDefinitionPublisher>();
builder.Services.AddScoped<AboutUsPublisher>();
builder.Services.AddScoped<HowItWorksPublisher>();
builder.Services.AddScoped<WebsitePublisher>();
builder.Services.AddScoped<CompanyPublisher>();

await builder.Build().RunAsync();
