using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.WebApi.Infrastructure;

public class GlobalExceptionHandler: IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Terjadi Galat: {Message}", exception.Message);

        if (exception is ValidationException validationException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"
            };
            
            var errors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());
            
            problemDetails.Extensions.Add("errors", errors);
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            
            return true;
        }
        
        var defaultProblemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Detail = "An unexpected error occurred."
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(defaultProblemDetails, cancellationToken);

        return true;
    }
}