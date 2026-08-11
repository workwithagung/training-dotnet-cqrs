using MediatR;

namespace UserManagement.Application.Commands;

public record DeletePegawaiCommand(Guid Id): IRequest;