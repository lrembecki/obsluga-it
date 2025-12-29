using lrembecki.core;
using lrembecki.core.Exceptions;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Helpers;
using lrembecki.function_app.Helpers;
using lrembecki.infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace lrembecki.function_app;

public class SubmitFormFunction(
    ISessionAccessor session,
    IFormDefinitionService formDefinitions,
    IFormService forms)
{

    [Function("SubmitFormFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", "get", Route = "submit-form/{subscriptionId}/{formDefinitionId}")] 
        HttpRequest req)
    {
        SubmitFormInputModel input = null!;
        Guid subscriptionId;

        try
        {
            if (!req.RouteValues.ContainsKey("SubscriptionId"))
                throw new Exception("Subscription not provided");

            if (!Guid.TryParse(req.RouteValues["SubscriptionId"]!.ToString(), out subscriptionId))
                throw new Exception("Invalid subscription");
        } 
        catch (Exception ex)
        {
            return new UnauthorizedObjectResult(ex.Message);
        }

        using (var scope = session.CreateSessionContext(subscriptionId))
        {
            input = await SubmitFormInputModel.Create(req, formDefinitions);

            if (req.Method == HttpMethods.Get)
            {
                return new OkObjectResult(input.FormDefinition.ToServiceCallResult());
            }
            else
            {
                Dictionary<string, string> model = null!;

                try
                {
                    model = (await req.ReadFromJsonAsync<Dictionary<string, string>>())!;
                    input.FormDefinition.Validate(model);
                }
                catch (ValidationException ex)
                {
                    return new BadRequestObjectResult(ex.ToFailServiceCallResult());
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ServiceCallResult.CreateExceptionResult(ex));
                }

                var fields = input.FormDefinition.Fields.Where(e => model.ContainsKey(e.FieldName))
                    .ToDictionary(e => e.FieldName, e => model[e.FieldName]);

                var form = new FormDto
                {
                    FormDefinitionId = input.FormDefinition.Id,
                    Fields = fields
                };

                var result = (await forms.CreateAsync(form)).ToServiceCallResult();

                return new OkObjectResult(result);
            }
        }

            
    }
}