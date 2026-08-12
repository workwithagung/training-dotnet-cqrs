using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Extensions;
using UserManagement.Application.Queries;
using UserManagement.Application.Responses;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Handlers;

public class SearchPegawaiQueryHandler: IRequestHandler<SearchPegawaiQuery, PagedResult<PegawaiResponse>>
{
    private readonly IPegawaiRepository _repository;

    public SearchPegawaiQueryHandler(IPegawaiRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<PegawaiResponse>> Handle(SearchPegawaiQuery request, CancellationToken cancellationToken)
    {
        
        var (data, totalCount) = await _repository.GetAllAsync(request.Keyword, request.Page, request.Size, cancellationToken);
        
        return new PagedResult<PegawaiResponse>( data.ConvertAll(p => p.ToResponse()), totalCount, request.Page, request.Size);
    }
}