using Common.Enums;

namespace BookLibrary.WebApi.Filters;

public class SortFilter
{
    public SortFilter()
    {
        SortOrder = SortOrder.Ascending;
        SortField = SortField.Id;
    }

    public SortOrder SortOrder { get; set; }
    public SortField SortField { get; set; }
}