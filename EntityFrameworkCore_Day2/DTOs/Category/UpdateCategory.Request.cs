using System.ComponentModel.DataAnnotations;

namespace ProductStore.Dtos;

public class UpdateCategoryRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
}