using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Shared;

namespace UserManagement.Application.Commands;

public record CreateJabatanCommand(string Nama, int Level): IRequest<Result<Jabatan>>;