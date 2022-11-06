using BookLibrary.WebApi.Dtos.User;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : BaseController
{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticationRequest requestModel)
    {
        try
        {
            var response = await _userService.Authenticate(requestModel);

            if (response == null)
                return BadRequest(ErrorMessages.LoginFailed);

            return Ok(response);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }
}