using Common.DataType;

namespace Common.Extensions;

public static class PagedListExtension
{
    public static object ToObject<T>(this IPagedList<T> pagedList)
    {
        return new
        {
            pagedList.PageIndex,
            pagedList.PageSize,
            pagedList.TotalPage,
            pagedList.TotalRecord,
            pagedList.HasNextPage,
            pagedList.HasPreviousPage,
            Data = pagedList
        };
    }
}