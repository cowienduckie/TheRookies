namespace Common.DataType;

[Serializable]
public class PagedList<T> : List<T> , IPagedList<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalRecord { get; }
    public int TotalPage { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPage;

    public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        TotalRecord = source.Count();
        TotalPage = TotalRecord / PageSize;

        if (TotalRecord % PageSize > 0) ++TotalPage;

        PageIndex = pageIndex > TotalPage ? TotalPage : pageIndex;

        AddRange(source.Skip((PageIndex - 1) * PageSize).Take(PageSize));
    }
}