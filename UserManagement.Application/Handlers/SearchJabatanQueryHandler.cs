using MediatR;
using UserManagement.Application.Common;
using UserManagement.Application.Extensions;
using UserManagement.Application.Queries;
using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Handlers;

public class SearchJabatanQueryHandler: IRequestHandler<SearchJabatanQuery, PagedResult<JabatanResponse>>
{
    private readonly IJabatanRepository _repository;

    public SearchJabatanQueryHandler(IJabatanRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<JabatanResponse>> Handle(SearchJabatanQuery request, CancellationToken cancellationToken)
    {
        var (data, totalCount) = await _repository.GetAllAsync(request.Keyword, request.Page, request.Size, cancellationToken);
        
        return new PagedResult<JabatanResponse>(data.ConvertAll(j => j.ToResponse()), totalCount, request.Page, request.Size);
    }
}