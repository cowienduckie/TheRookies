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

        var fileName = $"{DateTime.Now:yyMMddhhmmss}_Request.log";
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "Logs/", fileName);

        var fileContent = string.Empty;

        fileContent += $"Scheme: {request.Scheme}\n";
        fileContent += $"Host: {request.Host}\n";
        fileContent += $"Path: {request.Path}\n";
        fileContent += $"Query String: {request.QueryString}\n";
        fileContent += $"Request Body:\n{bodyString}\n";

        File.WriteAllText(filePath, fileContent);

        request.Body.Position = 0;

        await _next(context);
    }
}