using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands;
using UserManagement.Application.Queries;
using UserManagement.WebApi.Contracts.Requests;

namespace UserManagement.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PegawaiController : ApiController
{
    private readonly IMediator _mediator;

    public PegawaiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePegawai([FromBody] CreatePegawaiCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if(result.IsFailure) return HandleFailure(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPegawai([FromQuery] SearchPegawaiQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPegawaiByIdQuery(id), cancellationToken);
        
        if(result.IsFailure) return HandleFailure(result);
        
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePegawai(
        [FromRoute] Guid id, 
        [FromBody] PatchPegawaiRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PatchPegawaiCommand(id, request.Tunjangan, request.JabatanId);
        var result = await _mediator.Send(command, cancellationToken);
        
        if(result.IsFailure) return HandleFailure(result);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePegawai(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePegawaiCommand(id), cancellationToken);

        return NoContent();
    }
}