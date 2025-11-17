using lrembecki.host.Infrastructure;
using lrembecki.infrastructure;

using lrembecki.presentation.account;
using lrembecki.presentation.security;
using lrembecki.presentation.settings;
using lrembecki.presentation.storage;
using lrembecki.presentation.trotamundos;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAccount();
builder.AddStorage();
builder.AddSettings();
builder.AddTrotamundos();

builder.AddInfrastructure(
    builder.Environment.IsDevelopment(),
    builder.Configuration.GetConnectionString("AppConfiguration")!,
    builder.Configuration["EntraId:TenantId"]!);

builder.AddSecurity();

var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapAccount();
app.MapStorage();
app.MapSettings();
app.MapTrotamundos();
app.MapSecurity();

await app.RunAsync();
