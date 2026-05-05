namespace Domain.Entities.Common;

public class PagedResult<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)pageSize);
    public int TotalCount { get; set; } = totalCount;
    public int ItemsFrom { get; set; } = (pageNumber - 1) * pageSize + 1;
    public int ItemsTo { get; set; } = Math.Min(pageNumber * pageSize, totalCount);
}
