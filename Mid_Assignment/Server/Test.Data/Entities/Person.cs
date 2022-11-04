using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace Test.Data.Entities;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PersonId { get; set; }

    public string Name { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public GenderEnum? Gender { get; set; }

    public string? EmailAddress { get; set; }

    public string? LicenseId { get; set; }
}