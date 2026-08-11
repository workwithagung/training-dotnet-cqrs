using MediatR;
using UserManagement.Application.Queries;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class GetPegawaiByIdQueryHandler: IRequestHandler<GetPegawaiByIdQuery, Result<Pegawai>>
{
    private readonly IPegawaiRepository _repository;

    public GetPegawaiByIdQueryHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Pegawai>> Handle(GetPegawaiByIdQuery request, CancellationToken cancellationToken)
    {
        var pegawai = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (pegawai == null)
        {
            return Result<Pegawai>.Error("Pegawai tidak ditemukan");
        }
        
        return Result<Pegawai>.Success(pegawai, "Pegawai ditemukan.");
    }
}