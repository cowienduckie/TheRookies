using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.User;
using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtHelper _jwtHelper;

    public UserService(IUserRepository userRepository, IJwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync(user => user.Id == id);

        if (user == null) return null;

        return new UserModel
        {
            Id = user.Id,
            Role = user.Role
        };
    }

    public async Task<AuthenticationResponse?> Authenticate(AuthenticationRequest requestModel)
    {
        var user = await _userRepository
            .GetSingleAsync(user => user.Username == requestModel.Username &&
                                    user.Password == requestModel.Password);
        if (user == null)
        {
            return null;
        }

        var token = _jwtHelper.GenerateJwtToken(new UserModel
        {
            Id = user.Id,
            Role = user.Role
        });

        return new AuthenticationResponse
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            Role = user.Role.ToString(),
            Token = token
        };
    }
}