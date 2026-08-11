using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Queries;

public record GetPegawaiByIdQuery(Guid Id): IRequest<Result<Pegawai>>;