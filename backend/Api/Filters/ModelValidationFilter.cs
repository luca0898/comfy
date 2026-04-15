using System.Net;
using CrossCutting.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = new List<object>();

            for (var errorItemIndex = 0; errorItemIndex < context.ModelState.Keys.Count(); errorItemIndex++)
            {
                var errorKey = context.ModelState.Keys.ToList()[errorItemIndex];
                var errorValue = context.ModelState.Values.ToList()[errorItemIndex];

                errors.Add(new
                {
                    Field = char.ToLowerInvariant(errorKey[0]) + errorKey.Substring(1),
                    Reasons = errorValue.Errors.Select(error => error.ErrorMessage)
                });
            }

            var errorResponse = new ErrorResponseViewModel
            {
                ErrorCode = HttpStatusCode.BadRequest.ToString(),
                Message = "Model is invalid",
                Errors = errors
            };

            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
}