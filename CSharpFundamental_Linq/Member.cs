namespace CSharpFundamentalLinq;

public enum Gender
{
    Male,
    Female,
    Other
}

public class Member
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public int Age => CalculateAge(DateOfBirth);
    public bool IsGraduated { get; set; }
    public string FullName => LastName + " " + FirstName;

    public Member(
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

    public void PrintInfo()
    {
        var graduationStatus = IsGraduated ? "Graduated" : "Not graduated";

        Console.WriteLine($"First name: {FirstName} | Last name: {LastName} | Full name: {FullName}");
        Console.WriteLine($"Gender: {Gender} | DOB: {DateOfBirth:dd/MM/yyyy} | Birth place: {BirthPlace}");
        Console.WriteLine($"Phone: {PhoneNumber} | {graduationStatus}");
        Console.WriteLine();
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        return dateOfBirth > today.AddYears(-age) ? --age : age;
    }
}