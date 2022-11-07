using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class GetBorrowRequestRequest
{
    public UserModel Requester { get; set; } = null!;

    public int? Id { get; set; }
}