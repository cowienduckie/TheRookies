namespace CSharpFundamental;

public static class Solution
{
    private static readonly List<ClassMember> _classMembers = new()
    {
        new ClassMember("Minh", "Tran", Gender.Male, new DateTime(2000,04,24), "0962215871", "Hanoi", false),
        new ClassMember("Mai", "Tran", Gender.Female, new DateTime(2002,04,26), "0962215871", "Hanoi", true),
        new ClassMember("Hoa", "Tran", Gender.Other, new DateTime(1999,09,21), "0962215871", "Hanoi", true)
    };
    private const string _divider = "-------------------------------------------------";

    public static List<ClassMember> GetMembersByGender(Gender gender)
    {
        Console.WriteLine($"\nClass member with gender is {gender}:\n" +  _divider);
        List<ClassMember> results = new();

        foreach (var member in _classMembers)
        {
            if  (member.Gender == gender)
            {
                results.Add(member);
                PrintProfile(member);
            }
        }

        return results;
    }

    public static ClassMember GetOldestMember()
    {
        Console.WriteLine("\nOldest member in class:\n" +  _divider);
        var result = new ClassMember();

        foreach (var member in _classMembers)
        {
            if (member.Age > result.Age)
            {
                result = member;
            }
        }

        PrintProfile(result);
        return result;
    }

    public static List<string> GetAllFullNames()
    {
        Console.WriteLine("\nClass member full names:\n" +  _divider);
        var results = new List<string>();

        foreach (var member in _classMembers)
        {
            var fullName = member.LastName + " " + member.FirstName;

            results.Add(fullName);
            Console.WriteLine(fullName);
        }

        Console.WriteLine(_divider);
        return results;
    }

    public static void GetMembersGroupByBirthYear(
        out List<ClassMember> birthYearLessThan,
        out List<ClassMember> birthYearEqualTo,
        out List<ClassMember> birthYearGreaterThan,
        int queryYear
    )
    {
        birthYearLessThan = new();
        birthYearEqualTo = new();
        birthYearGreaterThan = new();

        foreach(var member in _classMembers)
        {
            var birthYear = member.DateOfBirth.Year;

            if (birthYear < queryYear)
            {
                birthYearLessThan.Add(member);
            }
            else if (birthYear == queryYear)
            {
                birthYearEqualTo.Add(member);
            }
            else if (birthYear > queryYear)
            {
                birthYearGreaterThan.Add(member);
            }
        }

        Console.WriteLine($"\nBirth year less than {queryYear}:\n" + _divider);
        birthYearLessThan.ForEach(m => PrintProfile(m));

        Console.WriteLine($"\nBirth year equal to {queryYear}:\n" + _divider);
        birthYearEqualTo.ForEach(m => PrintProfile(m));

        Console.WriteLine($"\nBirth year greater than {queryYear}:\n" + _divider);
        birthYearGreaterThan.ForEach(m => PrintProfile(m));
    }

    public static ClassMember GetFirstMemberBornIn(string birthPlace)
    {
        Console.WriteLine($"\nFirst member born in {birthPlace}:\n" +  _divider);

        foreach (var member in _classMembers)
        {
            if (member.BirthPlace == birthPlace)
            {
                PrintProfile(member);
                return member;
            }
        }

        Console.WriteLine($"Not found member born in {birthPlace}!\n" + _divider);
        return new ClassMember();
    }

    private static void PrintProfile(ClassMember member)
    {
        foreach (var prop in member.GetType().GetProperties())
        {
            Console.WriteLine($"{prop.Name}: {prop.GetValue(member, null)}");
        }

        Console.WriteLine(_divider);
    }
}