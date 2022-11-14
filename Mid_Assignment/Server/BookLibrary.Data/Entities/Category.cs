namespace BookLibrary.Data.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = null!;
}