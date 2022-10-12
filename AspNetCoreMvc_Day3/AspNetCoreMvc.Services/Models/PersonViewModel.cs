using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc.Services.Models;

public class PersonViewModel
{
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Display(Name = "Gender")]
    public string? Gender { get; set; }

    [Display(Name = "Date of Birth")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Phone")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Birth Place")]
    public string? BirthPlace { get; set; }
}