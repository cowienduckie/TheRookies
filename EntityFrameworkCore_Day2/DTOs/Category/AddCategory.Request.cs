using System.ComponentModel.DataAnnotations;

namespace ProductStore.Dtos;

public class AddCategoryRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
}