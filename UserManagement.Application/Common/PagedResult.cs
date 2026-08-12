namespace UserManagement.Application.Common;

public class PagedResult<T>
{
    public List<T> Data { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int Size { get; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / Size);
    
    public PagedResult(List<T> data, int totalCount, int page, int size)
    {
        Data = data;
        TotalCount = totalCount;
        Page = page;
        Size = size;
    }
}
