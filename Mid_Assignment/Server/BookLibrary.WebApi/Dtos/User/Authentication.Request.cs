using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.User;

public class AuthenticationRequest
{
    [Required] public string Username { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}