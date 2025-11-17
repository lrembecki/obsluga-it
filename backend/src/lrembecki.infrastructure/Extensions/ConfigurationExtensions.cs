using Azure.Core;
using Azure.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace lrembecki.infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    public static void AddAzureAppConfiguration(
        this IHostApplicationBuilder builder, 
        bool isDevelopment, 
        string appConfiguration, 
        string tenantId)
    {

        builder.Configuration.AddAzureAppConfiguration(ac =>
        {
            TokenCredential credential = isDevelopment
                ? new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    TenantId = tenantId
                })
                : new ManagedIdentityCredential(builder.Configuration["ClientId"]!);

            ac.ConfigureKeyVault(kv => kv.SetCredential(credential));
            ac.Connect(new Uri(appConfiguration!), credential);
        });
    }
}
