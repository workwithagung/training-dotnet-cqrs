using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class PatchPegawaiCommandHandler: IRequestHandler<PatchPegawaiCommand, Result<Pegawai>>
{
    private readonly IPegawaiRepository _repository;
    private readonly IJabatanRepository _jabatanRepository;

    public PatchPegawaiCommandHandler(IPegawaiRepository repository, IJabatanRepository jabatanRepository)
    {
        _repository = repository;
        _jabatanRepository = jabatanRepository;
    }

    public async Task<Result<Pegawai>> Handle(PatchPegawaiCommand request, CancellationToken cancellationToken)
    {
        var pegawai = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (pegawai == null)
        {
            return Result<Pegawai>.Error("pegawai not found");
        }

        var jabatan = await _jabatanRepository.GetByIdAsync(request.JabatanId, cancellationToken);

        if (jabatan == null)
        {
            return Result<Pegawai>.Error("ID Jabatan tidak valid.");
        }
        
        pegawai.UpdateDetails(jabatan, request.Tunjangan);
        await _repository.UpdateAsync(pegawai, cancellationToken);
        
        return Result<Pegawai>.Success(pegawai, "Update pegawai berhasil.");

    }
}