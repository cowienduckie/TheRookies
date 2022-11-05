using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class GetBorrowRequestResponse
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public UserModel RequestedBy { get; set; } = null!;
    public DateTime RequestedAt { get; set; }
    public UserModel? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public List<BookModel> Books { get; set; } = null!;
}