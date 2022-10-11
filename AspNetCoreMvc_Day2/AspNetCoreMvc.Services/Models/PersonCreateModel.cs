using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc.Services.Models;

public class PersonCreateModel
{
    [Required]
    [StringLength(12)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(12)]
    public string? LastName { get; set; }

    [Required]
    public string? Gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}