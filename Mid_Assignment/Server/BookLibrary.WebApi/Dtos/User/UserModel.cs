using Common.Enums;

namespace BookLibrary.WebApi.Dtos.User;

public class UserModel
{
    public int Id { get; set; }
    public Roles Role { get; set; }
}