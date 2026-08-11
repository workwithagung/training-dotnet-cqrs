using MediatR;
using UserManagement.Application.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Queries;

public record SearchPegawaiQuery(string Keyword = "", int Page = 0, int Size = 10): IRequest<PagedResult<Pegawai>>;