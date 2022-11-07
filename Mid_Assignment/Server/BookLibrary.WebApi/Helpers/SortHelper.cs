using Common.Enums;

namespace BookLibrary.WebApi.Helpers;

public static class SortHelper
{
    public static IEnumerable<T> SortData<T>(
        this IQueryable<T> source,
        IEnumerable<SortField> validSortFields,
        SortField sortField,
        SortOrder sortOrder) where T : class
    {
        if (!validSortFields.Contains(sortField)) return source;

        var prop = typeof(T).GetProperty(sortField.ToString());

        if (prop == null) return source;

        return sortOrder switch
        {
            SortOrder.Ascending => source.OrderBy(entity => prop.GetValue(entity)),
            SortOrder.Descending => source.OrderByDescending(entity => prop.GetValue(entity)),
            _ => source
        };
    }
}