using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Helpers;

public interface IJwtHelper
{
    public string GenerateJwtToken(UserModel user);
    public int? ValidateJwtToken(string? token);
}