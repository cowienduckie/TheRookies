using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class ApproveBorrowRequestResponse
{
    public int Id { get; set; }
    public string Status { get; set; }
    public UserModel Requester { get; set; }
    public DateTime RequestedAt { get; set; }
    public UserModel? Approver { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public List<BookModel> Books { get; set; }

    public ApproveBorrowRequestResponse(Data.Entities.BorrowRequest request)
    {
        Id = request.Id;
        Status = request.Status.ToString();
        RequestedAt = request.RequestedAt;
        Requester = new UserModel
        {
            Id = request.Requester.Id,
            Name = request.Requester.Name,
            Role = request.Requester.Role.ToString(),
            Username = request.Requester.Username
        };
        ApprovedAt = request.ApprovedAt;
        Approver = request.Approver != null
            ? new UserModel
            {
                Id = request.Approver.Id,
                Name = request.Approver.Name,
                Role = request.Approver.Role.ToString(),
                Username = request.Approver.Username
            }
            : null;
        Books = request.Books.Select(book => new BookModel
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description,
            Cover = book.Cover
        }).ToList();
    }
}