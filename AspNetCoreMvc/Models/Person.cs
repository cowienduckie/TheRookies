namespace AspNetCoreMvc.Models;

public enum Gender
{
    Male,
    Female,
    Other
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public bool IsGraduated { get; set; }
    public int Age => CalculateAge(DateOfBirth);
    public string FullName => LastName + " " + FirstName;

    public Person(
        string firstName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth,
        string phoneNumber,
        string birthPlace,
        bool isGraduated)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
        IsGraduated = isGraduated;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        return dateOfBirth > today.AddYears(-age) ? --age : age;
    }
}