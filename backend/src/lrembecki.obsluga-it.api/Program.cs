using lrembecki.obsluga_it.api.Endpoints;
using lrembecki.obsluga_it.application;
using lrembecki.obsluga_it.infrastructure;

using Microsoft.OpenApi.Models;

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

var endpointModules = typeof(IEndpointModule).Assembly
    .GetTypes()
    .Where(t => !t.IsAbstract && typeof(IEndpointModule).IsAssignableFrom(t))
    .Select(Activator.CreateInstance)
    .Cast<IEndpointModule>();

foreach (var m in endpointModules)
    m.MapEndpoints(app);

app.Run();
