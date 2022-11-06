using BookLibrary.WebApi.Dtos.Book;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class CreateBorrowRequestResponse
{
    public CreateBorrowRequestResponse(Data.Entities.BorrowRequest request)
    {
        Id = request.Id;
        Status = request.Status.ToString();
        RequestedBy = request.RequestedBy;
        RequestedAt = request.RequestedAt;

        Books = request.Books.Select(book => new BookModel
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description,
            Cover = book.Cover
        }).ToList();
    }

    public int Id { get; set; }
    public string Status { get; set; }
    public int RequestedBy { get; set; }
    public DateTime RequestedAt { get; set; }
    public List<BookModel> Books { get; set; }
}