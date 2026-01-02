using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lrembecki.presentation.forms;

public static class RegisterServices
{
    public static void AddForms(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFormDefinitionService, FormDefinitionService>();
        builder.Services.AddScoped<IFormService, FormService>();
        builder.Services.AddScoped<IFilterEvaluator<FormEntity, FormFilter>, FormFilterEvaluator>();
    }

    public static void MapForms(this WebApplication app)
    {
        var group = app.MapGroup("/api/forms")
            .WithTags("Forms")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IFormService, FormDto, FormVM, FormFilter>("forms");
        group.MapCrud<IFormDefinitionService, FormDefinitionDto, FormDefinitionVM>("form-definitions");
    }
}
