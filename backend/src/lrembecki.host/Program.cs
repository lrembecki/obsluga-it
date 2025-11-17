using lrembecki.host.Infrastructure;
using lrembecki.infrastructure;

using lrembecki.presentation.account;
using lrembecki.presentation.security;
using lrembecki.presentation.settings;
using lrembecki.presentation.storage;
using lrembecki.presentation.trotamundos;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var requestSize = 100L * 1024 * 1024;

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = requestSize; 
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = requestSize;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

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
