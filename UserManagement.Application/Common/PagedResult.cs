namespace UserManagement.Application.Common;

public class PagedResult<T>
{
    public List<T> Content { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int Size { get; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / Size);
    
    public PagedResult(List<T> content, int totalCount, int page, int size)
    {
        Content = content;
        TotalCount = totalCount;
        Page = page;
        Size = size;
    }
}
