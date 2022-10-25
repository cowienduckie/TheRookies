using System.ComponentModel.DataAnnotations;

namespace ProductStore.Dtos;

public class UpdateCategoryRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
}