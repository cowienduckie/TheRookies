using Common.Enums;

namespace BookLibrary.WebApi.Filters;

public class SortFilter
{
    public SortFilter()
    {
        Order = SortOrder.Ascending;
        Field = SortField.Id;
    }

    public SortOrder Order { get; set; }
    public SortField Field { get; set; }
}