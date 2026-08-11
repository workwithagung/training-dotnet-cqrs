using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Commands;

public record PatchPegawaiCommand(Guid Id, decimal Tunjangan, Guid JabatanId): IRequest<Result<Pegawai>>;