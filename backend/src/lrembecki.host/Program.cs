using lrembecki.host.Infrastructure;
using lrembecki.infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructure(
    builder.Environment.IsDevelopment(),
    builder.Configuration.GetConnectionString("AppConfiguration")!,
    builder.Configuration["EntraId:TenantId"]!);

var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapAccount();
app.MapForms();
app.MapSecurity();
app.MapStorage();
app.MapSettings();
app.MapTrotamundos();
// Add endpoint mappings from other modules here

await app.RunAsync();