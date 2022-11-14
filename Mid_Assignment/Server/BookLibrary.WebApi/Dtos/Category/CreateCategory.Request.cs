using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.Category;

public class CreateCategoryRequest
{
    [Required] public string Name { get; set; } = null!;
}