using Comfy.SystemObjects.Exceptions;
using Comfy.SystemObjects.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Comfy.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger(typeof(ExceptionMiddlewareExtensions));

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is ComfyApplicationException)
                        {
                            ComfyApplicationException comfyException = contextFeature.Error as ComfyApplicationException;
                            logger.LogError("There was an exception in the application: {comfyException}", comfyException);
                            context.Response.StatusCode = (int)comfyException.StatusCode;

                            ErrorResponseViewModel errorModel = new ErrorResponseViewModel
                            {
                                ErrorCode = comfyException.StatusCode.ToString(),
                                Message = contextFeature.Error.Message
                            };

                            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(errorModel));
                        }
                        else
                        {
                            logger.LogError($"Something went wrong: {contextFeature.Error}");
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            await context.Response.WriteAsync(new ErrorResponseViewModel()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                            }.ToString());
                        }
                    }
                });
            });
        }
    }
}
