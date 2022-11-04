using Microsoft.AspNetCore.Mvc;
using AspNetCoreAPi.Services;
using AspNetCoreAPi.Models;
using AspNetCoreAPi.Helpers;

namespace AspNetCoreApi.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("users/profile")]
    public ActionResult GetProfile()
    {
        var currentUser = (UserModel?) HttpContext.Items["User"];

        try
        {
            if (currentUser == null) return NotFound();

            var profile = _userService.GetById(currentUser.Id);

            return Ok(profile);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost("authentication/login")]
    public ActionResult<LoginResponseModel> Login([FromBody] LoginRequestModel requestModel)
    {
        try
        {
            var response = _userService.Login(requestModel);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private ActionResult HandleException(Exception exception)
    {
        Console.WriteLine(exception);

        return StatusCode(500, exception);
    }
}