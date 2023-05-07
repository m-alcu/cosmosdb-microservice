using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace CoreApi.Application.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = e.Message,
                Detail = e.StackTrace
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }

}
