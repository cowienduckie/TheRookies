namespace CSharpFundamental;

public class ClassMember
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public int Age => CalculateAge(DateOfBirth);
    public bool IsGraduated { get; set; }

    public ClassMember(
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

    public ClassMember()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        BirthPlace = string.Empty;
        DateOfBirth = DateTime.MaxValue;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        return dateOfBirth > today.AddYears(-age) ? --age : age;
    }
}

public enum Gender
{
    Male = 0,
    Female = 1,
    Other = 2
}