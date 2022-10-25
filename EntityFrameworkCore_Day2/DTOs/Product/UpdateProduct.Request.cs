using System.ComponentModel.DataAnnotations;

namespace ProductStore.Dtos;

public class UpdateProductRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string Manufacture { get; set; } = null!;
    [Required]
    public int CategoryId { get; set; }
}