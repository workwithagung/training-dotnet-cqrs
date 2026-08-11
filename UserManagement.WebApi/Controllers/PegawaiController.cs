using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;
using UserManagement.Application.Queries;

namespace UserManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PegawaiController : ControllerBase
{
    private readonly IMediator _mediator;

    public PegawaiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePegawai([FromBody] CreatePegawaiCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPegawaiByIdQuery(id), cancellationToken);
        
        return Ok(response);
    }
}