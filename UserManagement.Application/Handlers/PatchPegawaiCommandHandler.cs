using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.Extensions;
using UserManagement.Application.Responses;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class PatchPegawaiCommandHandler: IRequestHandler<PatchPegawaiCommand, Result<PegawaiResponse>>
{
    private readonly IPegawaiRepository _repository;
    private readonly IJabatanRepository _jabatanRepository;

    public PatchPegawaiCommandHandler(IPegawaiRepository repository, IJabatanRepository jabatanRepository)
    {
        _repository = repository;
        _jabatanRepository = jabatanRepository;
    }

    public async Task<Result<PegawaiResponse>> Handle(PatchPegawaiCommand request, CancellationToken cancellationToken)
    {
        var pegawai = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (pegawai == null)
        {
            return Result<PegawaiResponse>.Error("pegawai not found");
        }

        var jabatan = await _jabatanRepository.GetByIdAsync(request.JabatanId, cancellationToken);

        if (jabatan == null)
        {
            return Result<PegawaiResponse>.Error("ID Jabatan tidak valid.");
        }
        
        pegawai.UpdateDetails(jabatan, request.Tunjangan);
        await _repository.UpdateAsync(pegawai, cancellationToken);
        
        return Result<PegawaiResponse>.Success(pegawai.ToResponse(), "Update pegawai berhasil.");

    }
}