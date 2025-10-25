using lrembecki.obsluga_it.application;
using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(_ => _
        .AddDefaultPolicy(p => p
            .WithOrigins(builder.Configuration["Cors"]!.Split(Environment.NewLine))
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/account", (IAuthenticationService authentication, ClaimsPrincipal user) =>
{
    var email = user.Claims.First(c => c.Type is "emails" or "preferred_username" or ClaimTypes.Email).Value;
    return authentication.SignInAsync(email, null);
}).RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy);

app.MapGet("/api/account/{subscriptionId:guid}", (Guid subscriptionId, IAuthenticationService authentication, ClaimsPrincipal user) =>
{
    var email = user.Claims.First(c => c.Type is "emails" or "preferred_username" or ClaimTypes.Email).Value;
    return authentication.SignInAsync(email, subscriptionId);
}).RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy);

app.MapGet("/api/subscriptions", (ISender sender) =>
{
    return sender.SendAsync(new GetSubscriptions());
}).RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy);

app.Run();
