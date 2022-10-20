using System.ComponentModel.DataAnnotations;

namespace ProductStore.Data.Entities;

public class Product : BaseEntity<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string Manufacture { get; set; } = null!;
    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}