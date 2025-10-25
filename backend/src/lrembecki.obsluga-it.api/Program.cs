using lrembecki.obsluga_it.application;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.infrastructure;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, _ =>
    {
        _.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["OpenId:Secret"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuers = [builder.Configuration["OpenId:Issuer"]],
            ValidAudiences = [builder.Configuration["OpenId:Audience"]]
        };
    })
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"), jwtBearerScheme: "AzureAd");

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

builder.Services.AddServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "obsluga-it API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/subscriptions", (ISender sender) => sender.SendAsync(new GetSubscriptions())).RequireAuthorization();

app.Run();
