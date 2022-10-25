using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc.Services.Models;

public class PersonEditModel
{
    [Required]
    [StringLength(12)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(12)]
    public string LastName { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BirthPlace { get; set; }
}