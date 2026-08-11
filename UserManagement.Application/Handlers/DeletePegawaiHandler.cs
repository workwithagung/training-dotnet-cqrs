using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Handlers;

public class DeletePegawaiHandler: IRequestHandler<DeletePegawaiCommand>
{
    private readonly IPegawaiRepository _repository;

    public DeletePegawaiHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeletePegawaiCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id, cancellationToken);
    }
}