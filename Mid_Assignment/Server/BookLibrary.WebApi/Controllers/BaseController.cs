using BookLibrary.WebApi.Dtos.User;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected UserModel? CurrentUser => (UserModel?) HttpContext.Items[Settings.CurrentUserContextKey];

    protected ActionResult HandleException(Exception exception)
    {
        Console.WriteLine(exception);

        return StatusCode(500, ErrorMessages.InternalServerError);
    }
}