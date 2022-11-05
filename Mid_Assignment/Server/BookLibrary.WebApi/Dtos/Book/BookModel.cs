namespace BookLibrary.WebApi.Dtos.Book;

public class BookModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Cover { get; set; }

}