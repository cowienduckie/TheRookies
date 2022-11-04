using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAPi.Models;

public class LoginResponseModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}