using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Shared;

namespace UserManagement.WebApi.Controllers;

[ApiController]
public abstract class ApiController: ControllerBase
{
    protected IActionResult HandleFailure<T>(Result<T> result)
    {
        return result.Error.Type switch
        {
            ErrorType.Validation => BadRequest(result),
            ErrorType.NotFound => NotFound(result),
            ErrorType.Conflict => Conflict(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, result)
        };
    }
}