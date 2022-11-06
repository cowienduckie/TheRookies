using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Microsoft.Extensions.Options;

namespace BookLibrary.WebApi.Middlewares;

public class JwtMiddleware
{
    private readonly AppSettings _appSettings;
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtHelper jwtHelper)
    {
        var token = context.Request.Headers[Settings.AuthorizationRequestHeader]
            .FirstOrDefault()
            ?.Split(" ")
            .Last();

        var userId = jwtHelper.ValidateJwtToken(token);

        if (userId != null)
            context.Items[Settings.CurrentUserContextKey] = await userService.GetByIdAsync(userId.Value);

        await _next(context);
    }
}