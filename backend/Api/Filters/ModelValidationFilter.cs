using Comfy.SystemObjects.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Comfy.API.Middlewares
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var errors = new List<object>();

                for (int errorItemIndex = 0; errorItemIndex < context.ModelState.Keys.Count(); errorItemIndex++)
                {
                    var errorKey = context.ModelState.Keys.ToList()[errorItemIndex];
                    var errorValue = context.ModelState.Values.ToList()[errorItemIndex];

                    errors.Add(new
                    {
                        Field = char.ToLowerInvariant(errorKey[0]) + errorKey.Substring(1),
                        Reasons = errorValue.Errors.Select(error => error.ErrorMessage)
                    });
                }

                ErrorResponseViewModel errorResponse = new ErrorResponseViewModel
                {
                    ErrorCode = HttpStatusCode.BadRequest.ToString(),
                    Message = "Model is invalid",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
