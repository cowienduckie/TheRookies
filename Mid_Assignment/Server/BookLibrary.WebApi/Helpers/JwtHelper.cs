using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookLibrary.WebApi.Dtos.User;
using Common.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookLibrary.WebApi.Helpers;

public class JwtHelper : IJwtHelper
{
    private const string _tokenClaimType = "id";
    private readonly byte[] _securityKey;

    public JwtHelper(IOptions<AppSettings> appSettings)
    {
        _securityKey = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
    }

    public string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(_tokenClaimType, user.Id.ToString())
            }),

            Expires = DateTime.UtcNow
                .AddSeconds(Settings.JwtTokenExpiredTimeInSecond),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_securityKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public int? ValidateJwtToken(string? token)
    {
        if (token == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_securityKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken);

            var jwtToken = (JwtSecurityToken) validatedToken;

            var userId = int.Parse(jwtToken.Claims.First(c => c.Type == _tokenClaimType).Value);

            return userId;
        }
        catch
        {
            return null;
        }
    }
}