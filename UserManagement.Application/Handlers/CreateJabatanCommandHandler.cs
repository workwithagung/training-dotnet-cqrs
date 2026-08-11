using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class CreateJabatanCommandHandler: IRequestHandler<CreateJabatanCommand, Result<Jabatan>>
{
    private readonly IJabatanRepository _repository;

    public CreateJabatanCommandHandler(IJabatanRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Jabatan>> Handle(CreateJabatanCommand request, CancellationToken cancellationToken)
    {
        var jabatan = new Jabatan()
        {
            Nama = request.Nama,
            Level = request.Level,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now
        };

        await _repository.AddAsync(jabatan, cancellationToken);
        
        return Result<Jabatan>.Success(jabatan, "Jabatan berhasil direkam.");
    }
}