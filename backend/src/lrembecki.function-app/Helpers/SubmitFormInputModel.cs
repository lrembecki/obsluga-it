using lrembecki.core.forms.FormDefinitions;
using Microsoft.AspNetCore.Http;

namespace lrembecki.function_app.Helpers;

internal record SubmitFormInputModel(FormDefinitionVM FormDefinition)
{
    public static async Task<SubmitFormInputModel> Create(HttpRequest req, IFormDefinitionService formDefinitions)
    {
        if (!req.RouteValues.ContainsKey("formDefinitionId"))
            throw new Exception("Form definition not provided");

        if (!Guid.TryParse(req.RouteValues["formDefinitionId"]!.ToString(), out Guid formDefinitionId))
            throw new Exception("Invalid form definition");

        var formDefinition = await formDefinitions.GetByIdAsync(formDefinitionId);
        if (formDefinition is null) throw new Exception("Form definition not found");

        return new SubmitFormInputModel(formDefinition);
    }
}
