using MediatR;
using UserManagement.Application.Extensions;
using UserManagement.Application.Queries;
using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class GetPegawaiByIdQueryHandler: IRequestHandler<GetPegawaiByIdQuery, Result<PegawaiResponse>>
{
    private readonly IPegawaiRepository _repository;

    public GetPegawaiByIdQueryHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PegawaiResponse>> Handle(GetPegawaiByIdQuery request, CancellationToken cancellationToken)
    {
        var pegawai = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (pegawai == null)
        {
            return Result<PegawaiResponse>.Error("Pegawai tidak ditemukan");
        }
        
        return Result<PegawaiResponse>.Success(pegawai.ToResponse(), "Pegawai ditemukan.");
    }
}