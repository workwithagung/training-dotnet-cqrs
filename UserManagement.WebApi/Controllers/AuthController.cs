using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Common.Interfaces;
using UserManagement.WebApi.Contracts.Responses;

namespace UserManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController: ApiController
{
    private readonly ICurrentUserService _currentUserService;

    public AuthController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public IActionResult WhoAmI()
    {
        return Ok(new GetWhoAmIResponse(
            _currentUserService.UserId,
            _currentUserService.UserName,
            _currentUserService.Roles,
            _currentUserService.Claims
            ));
    }
}