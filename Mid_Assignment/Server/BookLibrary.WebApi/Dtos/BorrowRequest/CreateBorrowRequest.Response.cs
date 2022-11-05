using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class CreateBorrowRequestResponse
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public UserModel RequestedBy { get; set; } = null!;
    public DateTime RequestedAt { get; set; }
    public List<BookModel> Books { get; set; } = null!;
}