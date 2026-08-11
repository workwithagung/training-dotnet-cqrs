using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Commands;

public record CreatePegawaiCommand(
    string Nip, 
    string Nama,
    decimal Tunjangan,
    Guid JabatanId
    ): IRequest<Result<Pegawai>>;