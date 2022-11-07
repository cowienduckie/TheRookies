using Common.Enums;

namespace BookLibrary.WebApi.Filters;

public class SortFilter
{
    public SortOrder Order { get; set; }
    public SortField Field { get; set; }

    public SortFilter()
    {
        Order = SortOrder.Ascending;
        Field = SortField.Id;
    }
}