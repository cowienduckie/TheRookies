using Common.Constants;

namespace BookLibrary.WebApi.Filters;

public class PagingFilter
{
    private int _pageIndex;
    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value > 0 ? value : Settings.DefaultPageIndex;
    }
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : Settings.DefaultPageSize;
    }


    public PagingFilter()
    {
        PageIndex = Settings.DefaultPageIndex;
        PageSize = Settings.DefaultPageSize;
    }
}