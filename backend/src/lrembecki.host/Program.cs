using lrembecki.core.Helpers;
using lrembecki.host.Infrastructure;
using lrembecki.infrastructure;
using lrembecki.infrastructure.Extensions;

using lrembecki.presentation.account;
using lrembecki.presentation.security;
using lrembecki.presentation.storage;
using lrembecki.presentation.trotamundos;

using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAccount();
builder.AddStorage();
builder.AddTrotamundos();
builder.AddInfrastructure();
builder.AddSecurity();

var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

//if (app.Environment.IsDevelopment())
//{
//    app.Use(async (ctx, next) =>
//    {
//        if (ctx.User?.Identity?.IsAuthenticated != true)
//        {
//            var tokenFactory = ctx.RequestServices.GetRequiredService<IJwtTokenFactory>();
//            ctx.User = new ClaimsPrincipal([
//                new ClaimsIdentity([
//                    new ("scp", "access_as_user"),
//                    new (ClaimTypes.NameIdentifier, builder.Configuration["Dev:UserId"]!),
//                    new (ClaimTypes.Email, builder.Configuration["Dev:Email"]!)
//                ], AuthenticationExtensions.AzureAdScheme),
//                tokenFactory.GetClaimsIdentity(
//                    Guid.Parse(builder.Configuration["Dev:SubscriptionId"]!),
//                    Guid.Parse(builder.Configuration["Dev:UserId"]!),
//                    "dev@dev.test",
//                    [],
//                    DateTime.UtcNow,
//                    DateTime.UtcNow.AddDays(15)
//                )
//            ]);
//        }

//        await next();
//    });
//}

app.UseAuthentication();
app.UseAuthorization();

app.MapAccount();
app.MapStorage();
app.MapTrotamundos();
app.MapSecurity();

await app.RunAsync();
