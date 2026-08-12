using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Shared;

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
            
            var validationErrors = validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            var errorResult = new ValidationError<Dictionary<string, string[]>>("V-01", "Request tidak valid.", validationErrors);
            var response = Result<ValidationError<ProblemDetails>>.Failure(errorResult);
            
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            
            return true;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(
            Result<object?>.Failure(Error.General("G-01", "Terjadi permasalahan pada server.")), 
            cancellationToken);

        return true;
    }
}