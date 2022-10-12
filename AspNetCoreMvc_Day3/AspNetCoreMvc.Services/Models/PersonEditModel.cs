using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc.Services.Models;

public class PersonEditModel
{
    [Required]
    [StringLength(12)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(12)]
    public string LastName { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BirthPlace { get; set; }
}