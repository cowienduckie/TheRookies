using AspNetCoreFundamental.Middlewares;

namespace AspNetCoreFundamental.Extensions;

public static class LoggingRequestMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingRequest(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingRequestMiddleware>();
    }
}