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

    private static void PrintProfile(ClassMember member)
    {
        foreach (var prop in member.GetType().GetProperties())
        {
            Console.WriteLine($"{prop.Name}: {prop.GetValue(member, null)}");
        }

        Console.WriteLine(_divider);
    }
}