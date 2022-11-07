using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.Category;

public class UpdateCategoryRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}