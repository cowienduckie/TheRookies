using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreAPi.Helpers;
using AspNetCoreAPi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreAPi.Services;

public class UserService : IUserService
{
    private readonly List<UserModel> _users = new()
    {
        new UserModel { Id = 1, Username = "test", Password = "test" }
    };

    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public UserModel? GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    public LoginResponseModel? Login(LoginRequestModel requestModel)
    {
        var user = _users.SingleOrDefault(x => x.Username == requestModel.Username && x.Password == requestModel.Password);

        if (user == null) return null;

        var token = GenerateJwtToken(user);

        return new LoginResponseModel
        {
            Id = user.Id,
            Username = user.Username,
            Token = token
        };
    }

    private string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}