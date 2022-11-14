namespace Common.Constants;

public static class Settings
{
    public const int MaxBorrowRequestsPerMonth = 3;
    public const int MaxBooksPerRequest = 5;
    public const int MinBooksPerRequest = 1;
    public const int DefaultPageSize = 10;
    public const int DefaultPageIndex = 1;
    public const string BaseBookCoverUrl = "https://dummyimage.com/300x450/";
    public const string AuthorizationRequestHeader = "Authorization";
    public const string CurrentUserContextKey = "CurrentUser";
}