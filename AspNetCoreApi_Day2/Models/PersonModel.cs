namespace AspNetCoreAPi.Models;

public enum Gender
{
    Male = 0,
    Female = 1,
    Other = 2
}

public class PersonModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string BirthPlace { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public PersonModel(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string birthPlace
    )
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BirthPlace = birthPlace;
        Gender = gender == "Male" ? Gender.Male
            : gender == "Female" ? Gender.Female
            : Gender.Other;
    }
}