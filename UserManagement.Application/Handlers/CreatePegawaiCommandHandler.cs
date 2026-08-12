using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;
using UserManagement.Application.Extensions;

namespace UserManagement.Application.Handlers;

public class CreatePegawaiCommandHandler: IRequestHandler<CreatePegawaiCommand, Result<PegawaiResponse>>
{
    private readonly IPegawaiRepository _repository;
    private readonly IJabatanRepository _jabatanRepository;

    public CreatePegawaiCommandHandler(IPegawaiRepository repository, IJabatanRepository jabatanRepository)
    {
        _repository = repository;
        _jabatanRepository = jabatanRepository;
    }

    
    public async Task<Result<PegawaiResponse>> Handle(CreatePegawaiCommand request, CancellationToken cancellationToken)
    {
        var jabatan = await _jabatanRepository.GetByIdAsync(request.JabatanId, cancellationToken);
        
        if (jabatan == null) return Result<PegawaiResponse>.Error("Jabatan not found.");
        
        var pegawai = new Pegawai()
        {
            Nama = request.Nama,
            Nip = request.Nip,
            Tunjangan = request.Tunjangan,
            Jabatan = jabatan
        };
        
        await _repository.AddAsync(pegawai, cancellationToken);
        
        return Result<PegawaiResponse>.Success(pegawai.ToResponse(), "Create pegawai success.");
    }
}