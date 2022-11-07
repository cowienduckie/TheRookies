using Common.Constants;

namespace BookLibrary.WebApi.Filters;

public class PagingFilter
{
    private int _pageIndex;
    private int _pageSize;

    public PagingFilter()
    {
        PageIndex = Settings.DefaultPageIndex;
        PageSize = Settings.DefaultPageSize;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value > 0 ? value : Settings.DefaultPageIndex;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : Settings.DefaultPageSize;
    }
}