using System.ComponentModel.DataAnnotations;

namespace ProductStore.Data.Entities;

public class Category : BaseEntity<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
}