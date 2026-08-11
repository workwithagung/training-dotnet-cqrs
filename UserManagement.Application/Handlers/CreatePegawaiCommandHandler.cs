using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Handlers;

public class CreatePegawaiCommandHandler: IRequestHandler<CreatePegawaiCommand, Result<Pegawai>>
{
    private readonly IPegawaiRepository _repository;

    public CreatePegawaiCommandHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<Result<Pegawai>> Handle(CreatePegawaiCommand request, CancellationToken cancellationToken)
    {
        var pegawai = new Pegawai()
        {
            Nama = request.Nama,
            Nip = request.Nip,
            Tunjangan = request.Tunjangan,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now
        };
        
        await _repository.AddAsync(pegawai, cancellationToken);
        
        return Result<Pegawai>.Success(pegawai, "Create pegawai success.");
    }
}