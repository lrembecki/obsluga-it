using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace lrembecki.infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    public static void AddAzureAppConfiguration(this WebApplicationBuilder builder)
    {

        builder.Configuration.AddAzureAppConfiguration(ac =>
        {
            TokenCredential credential = builder.Environment.IsDevelopment()
                ? new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    TenantId = builder.Configuration["EntraId:TenantId"]
                })
                : new ManagedIdentityCredential(clientId: builder.Configuration["ClientId"]);

            ac.ConfigureKeyVault(kv => kv.SetCredential(credential));
            ac.Connect(new Uri(builder.Configuration.GetConnectionString("AppConfiguration")!), credential);
        });
    }
}
