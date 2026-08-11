using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Queries;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Handlers;

public class SearchPegawaiQueryHandler: IRequestHandler<SearchPegawaiQuery, PagedResult<Pegawai>>
{
    private readonly IPegawaiRepository _repository;

    public SearchPegawaiQueryHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Pegawai>> Handle(SearchPegawaiQuery request, CancellationToken cancellationToken)
    {
        
        var (data, totalCount) = await _repository.GetAllAsync(request.Keyword, request.Page, request.Size, cancellationToken);
        
        return new PagedResult<Pegawai>(data, totalCount, request.Page, request.Size);
    }
}