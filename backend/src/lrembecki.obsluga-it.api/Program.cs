using lrembecki.obsluga_it.api;
using lrembecki.obsluga_it.api.Infrastructure;
using lrembecki.obsluga_it.application;
using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(_ => _
    .AddDefaultPolicy(p => 
        p.WithOrigins(builder.Configuration["Cors"]!.Split(Environment.NewLine))
            .AllowAnyMethod()
            .AllowAnyHeader()
        )
    )
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "obsluga-it API", Version = "v1" });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "obsluga-it API V1");
    c.RoutePrefix = string.Empty;
});
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    var devEmail = builder.Configuration["Dev:Email"] ?? "dev@local.test";
    var userVM = new UserVM(Guid.NewGuid(), devEmail, [new SubscriptionVM(Guid.Parse("999CDDA0-FC38-4030-B329-11CB867D38DE"), "Test")]);

    app.Use(async (ctx, next) =>
    {
        if (ctx.User?.Identity?.IsAuthenticated != true)
        {
            var tokenFactory = ctx.RequestServices.GetRequiredService<IJwtTokenFactory>();
            ctx.User = new ClaimsPrincipal([
                new ClaimsIdentity([
                    new (ClaimTypes.Email, devEmail),
                    new ("scp", "access_as_user"),
                    new (ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                ], AuthenticationExtensions.AzureAdScheme),
                tokenFactory.GetClaimsIdentity(
                    userVM,
                    userVM.Subscriptions[0],
                    [],
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(15)
                )
            ]);
        }

        await next();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var endpointModules = typeof(IEndpointModule).Assembly
    .GetTypes()
    .Where(t => !t.IsAbstract && typeof(IEndpointModule).IsAssignableFrom(t))
    .Select(Activator.CreateInstance)
    .Cast<IEndpointModule>();

foreach (var m in endpointModules)
    m.MapEndpoints(app);

await app.RunAsync();
