using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IUserService
{
    Task<UserModel?> GetByIdAsync(int id);
    Task<AuthenticationResponse?> Authenticate(AuthenticationRequest requestModel);
}