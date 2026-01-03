using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Services;
using lrembecki.infrastructure.shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapForms
{
    public static IServiceCollection AddForms(this IServiceCollection services)
    {
        services.AddScoped<IFormService, FormService>();
        services.AddScoped<IFormDefinitionService, FormDefinitionService>();
        services.AddScoped<IFilterEvaluator<FormEntity, FormFilter>, FormFilterEvaluator>();

        return services;
    }

    public static WebApplication MapForms(this WebApplication app)
    {
        var group = app.MapGroup("/api/forms")
            .WithTags("Forms")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IFormService, FormDto, FormVM, FormFilter>("forms");
        group.MapCrud<IFormDefinitionService, FormDefinitionDto, FormDefinitionVM>("form-definitions");

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromForms(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapForms).Assembly);
        return modelBuilder;
    }
}
