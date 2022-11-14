namespace Common.DataType;

public interface IPagedList<T> : IList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalRecord { get; }
    int TotalPage { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}