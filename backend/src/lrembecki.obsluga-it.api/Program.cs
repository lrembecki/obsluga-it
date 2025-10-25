using lrembecki.obsluga_it.application;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddServices();
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/subscriptions", (ISender sender) => sender.SendAsync(new GetSubscriptions()));

app.Run();
