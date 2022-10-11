namespace AspNetCoreMvc.DataAccess.Entities;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public int Age => CalculateAge(DateOfBirth);
    public string FullName => LastName + " " + FirstName;

    public Person(
        string firstName,
        string lastName,
        int gender,
        DateTime dateOfBirth,
        string phoneNumber,
        string birthPlace)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        BirthPlace = birthPlace;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        return dateOfBirth > today.AddYears(-age) ? --age : age;
    }
}