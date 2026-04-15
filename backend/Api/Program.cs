using Api.Middlewares;
using Scalar.AspNetCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHealthChecks();
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseHttpsRedirection();
        app.ConfigureExceptionHandler();
        app.UseAuthorization();
        app.Run();
    }
}