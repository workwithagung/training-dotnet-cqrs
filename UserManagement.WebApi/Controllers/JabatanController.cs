using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;
using UserManagement.Application.Queries;

namespace UserManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JabatanController: ApiController
{
    private readonly IMediator _mediator;

    public JabatanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateJabatanCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if(result.IsFailure) return HandleFailure(result);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SearchJabatanQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}