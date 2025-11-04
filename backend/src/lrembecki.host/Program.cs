using lrembecki.core.Helpers;
using lrembecki.host.Infrastructure;
using lrembecki.infrastructure;
using lrembecki.infrastructure.Extensions;
using lrembecki.presentation;
using lrembecki.presentation.account;
using lrembecki.presentation.security;
using lrembecki.presentation.settings;
using lrembecki.presentation.storage;
using lrembecki.presentation.trotamundos;

using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAccount();
builder.AddStorage();
builder.AddSettings();
builder.AddTrotamundos();
builder.AddInfrastructure();
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
