using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.Book;

public class CreateBookRequest
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public string? Cover { get; set; }

    [Required]
    public List<int> CategoryIds { get; set; } = null!;
}