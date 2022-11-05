namespace BookLibrary.WebApi.Dtos.User;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;
}