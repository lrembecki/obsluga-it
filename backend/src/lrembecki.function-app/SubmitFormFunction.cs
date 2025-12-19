using lrembecki.core;
using lrembecki.core.Exceptions;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lrembecki.function_app;

public class SubmitFormFunction(
    PredefinedSessionAccessor session,
    ILogger<SubmitFormFunction> logger,
    IFormDefinitionService formDefinitions,
    IFormService forms)
{
    private record SubmitFormInputModel(FormDefinitionVM FormDefinition)
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

    [Function("SubmitFormFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", "get", Route = "submit-form/{subscriptionId}/{formDefinitionId}")] 
        HttpRequest req)
    {
        if (!req.RouteValues.ContainsKey("SubscriptionId")) throw new Exception("Subscription not provided");

        if (!Guid.TryParse(req.RouteValues["SubscriptionId"]!.ToString(), out Guid subscriptionId))
            throw new Exception("Invalid subscription");


        SubmitFormInputModel input = null!;
        using var scope = session.CreateSessionContext(subscriptionId);

        try
        {
            input = await SubmitFormInputModel.Create(req, formDefinitions);
        } 
        catch (Exception ex)
        {
            return new UnauthorizedObjectResult(ex.Message);
        }

        if (req.Method == HttpMethods.Get)
        {
            return new OkObjectResult(input.FormDefinition.ToServiceCallResult());
        }
        else
        {
            var model = (await req.ReadFromJsonAsync<Dictionary<string, string>>())!;

            try
            {
                input.FormDefinition.Validate(model);
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(ex.ToFailServiceCallResult());
            }

            var fields = input.FormDefinition.Fields.Where(e => model.ContainsKey(e.FieldName))
                .ToDictionary(e => e.FieldName, e => model[e.FieldName]);

            var form = new FormDto
            {
                FormDefinitionId = input.FormDefinition.Id,
                Fields = fields
            };

            return new OkObjectResult(await forms.CreateAsync(form));
        }
    }
}