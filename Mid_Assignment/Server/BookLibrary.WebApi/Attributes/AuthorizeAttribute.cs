using BookLibrary.WebApi.Dtos.User;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookLibrary.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<Role> _roles;

    public AuthorizeAttribute(params Role[]? roles)
    {
        _roles = roles ?? Array.Empty<Role>();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAnonymousAllowed = context
            .ActionDescriptor
            .EndpointMetadata
            .OfType<AllowAnonymousAttribute>()
            .Any();

        if (isAnonymousAllowed) return;

        var user = (UserModel?) context.HttpContext.Items[Settings.CurrentUserContextKey];

        if (user == null || (_roles.Count > 0 && !_roles.Contains(user.Role)))
        {
            context.Result = new JsonResult(new {message = ErrorMessages.Unauthorized})
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}