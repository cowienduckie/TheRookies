using System.Text;

namespace AspNetCoreFundamental.Middlewares;

public class LoggingRequestMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        request.EnableBuffering();

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        await request.Body.ReadAsync(buffer);

        var bodyString = Encoding.UTF8.GetString(buffer);

        var logName = $"{DateTime.Now:yyMMddhhmmss}_Request.log";
        var logPath = Path.Combine(
            Directory.GetCurrentDirectory(), "Logs/", logName);

        var logContent = string.Empty;

        logContent += $"Scheme: {request.Scheme}\n";
        logContent += $"Host: {request.Host}\n";
        logContent += $"Path: {request.Path}\n";
        logContent += $"Query String: {request.QueryString}\n";
        logContent += $"Request Body:\n{bodyString}\n";

        File.WriteAllText(logPath, logContent);

        request.Body.Position = 0;

        await _next(context);
    }
}