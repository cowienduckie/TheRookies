using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Dtos.Book;

public class CreateBookResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public List<CategoryModel> Categories { get; set; } = null!;
}