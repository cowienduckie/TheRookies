namespace Common.Constants;

public static class ErrorMessages
{
    public const string InternalServerError = "Internal Server Error!";
    public const string BooksPerRequestLimitNotReached = "Minimum books per request not reached!";
    public const string BooksPerRequestLimitExceeded = "Books per request limit exceeded!";
    public const string RequestsPerMonthLimitExceeded = "Requests per month limit exceeded!";
    public const string LoginFailed = "Username or password is incorrect!";
    public const string Unauthorized = "Unauthorized";
    public const string RequestHasNoRequester = "Request has no requester!";
}