using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class CreatePegawaiCommandHandler: IRequestHandler<CreatePegawaiCommand, Result<Pegawai>>
{
    private readonly IPegawaiRepository _repository;
    private readonly IJabatanRepository _jabatanRepository;

    public CreatePegawaiCommandHandler(IPegawaiRepository repository, IJabatanRepository jabatanRepository)
    {
        _repository = repository;
        _jabatanRepository = jabatanRepository;
    }

    
    public async Task<Result<Pegawai>> Handle(CreatePegawaiCommand request, CancellationToken cancellationToken)
    {
        var jabatan = await _jabatanRepository.GetByIdAsync(request.JabatanId, cancellationToken);
        
        if (jabatan == null) return Result<Pegawai>.Error("Jabatan not found.");
        
        var pegawai = new Pegawai()
        {
            Nama = request.Nama,
            Nip = request.Nip,
            Tunjangan = request.Tunjangan,
            Jabatan = jabatan,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now
        };
        
        await _repository.AddAsync(pegawai, cancellationToken);
        
        return Result<Pegawai>.Success(pegawai, "Create pegawai success.");
    }
}