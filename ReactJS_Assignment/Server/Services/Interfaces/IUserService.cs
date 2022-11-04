using AspNetCoreAPi.Models;

namespace AspNetCoreAPi.Services;

public interface IUserService
{
    UserModel? GetById(int id);
    LoginResponseModel? Login(LoginRequestModel requestModel);
}