using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAPi.Models;

public class LoginRequestModel
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}