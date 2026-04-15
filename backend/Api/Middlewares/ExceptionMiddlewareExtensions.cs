using System.Net;
using System.Net.Mime;
using System.Text.Json;
using CrossCutting.Exceptions;
using CrossCutting.ViewModel;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    if (contextFeature.Error is ComfyApplicationException)
                    {
                        var comfyException = contextFeature.Error as ComfyApplicationException;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        var errorModel = new ErrorResponseViewModel
                        {
                            ErrorCode = nameof(HttpStatusCode.BadRequest),
                            Message = contextFeature.Error.Message,
                            Errors = contextFeature.Error.Message
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(errorModel));
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        await context.Response.WriteAsync(new ErrorResponseViewModel
                        {
                            Message = "Internal Server Error.",
                            ErrorCode = nameof(HttpStatusCode.InternalServerError),
                            Errors = contextFeature.Error.Message
                        }.ToString());
                    }
                }
            });
        });
    }
}