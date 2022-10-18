using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [Required]
    public string State { get; set; } = null!;
}