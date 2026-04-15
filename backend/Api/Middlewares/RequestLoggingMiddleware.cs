namespace Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation(
            $"Route '{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}' was requested");
        await _next(context);
    }
}