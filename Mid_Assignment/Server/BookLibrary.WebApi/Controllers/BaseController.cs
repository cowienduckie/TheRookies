using BookLibrary.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected User? CurrentUser => (User?) HttpContext.Items["User"];

    protected ActionResult HandleException(Exception exception)
    {
        Console.WriteLine(exception);

        return StatusCode(500, "Internal Server Error");
    }
}