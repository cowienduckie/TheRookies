using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAPi.Models;

public class PersonUpdateModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Gender { get; set; } = string.Empty;
    [Required]
    public string BirthPlace { get; set; } = string.Empty;
}