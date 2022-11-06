using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class GetBorrowRequestResponse
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
    public UserModel Requester { get; set; } = null!;
    public DateTime RequestedAt { get; set; }
    public UserModel? Approver { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public List<BookModel> Books { get; set; } = null!;

    public GetBorrowRequestResponse(Data.Entities.BorrowRequest request)
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
        Books = request.Books.Select(book => new BookModel
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description,
            Cover = book.Cover
        }).ToList();

        if (request.Approver != null)
        {
            ApprovedAt = request.ApprovedAt;
            Approver = new UserModel
            {
                Id = request.Approver.Id,
                Name = request.Approver.Name,
                Role = request.Approver.Role.ToString(),
                Username = request.Approver.Username
            };
        }
    }
}