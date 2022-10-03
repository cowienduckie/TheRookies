namespace CSharpFundamentalLinq;

public static class Solution
{
    private const string _divider = "-------------------------------------------------\n";

    private static readonly List<Member> _classMembers = new()
    {
        new Member("Minh", "Tran", Gender.Male, new DateTime(2000, 04, 24), "0962215871", "Ha Noi", false),
        new Member("Nam", "Nguyen", Gender.Male, new DateTime(2000, 09, 24), "0962215871", "Hoa Binh", false),
        new Member("Mai", "Tran", Gender.Female, new DateTime(2002, 04, 26), "0962215871", "Ha Noi", true),
        new Member("Hoa", "Tran", Gender.Other, new DateTime(1999, 09, 21), "0962215871", "Phu Tho", true)
    };

    public static void ShowMenu()
    {
        bool isExit = false;

        while (!isExit)
        {
            Console.Clear();
            Console.WriteLine("C# Fundamentals Assignment #1");
            Console.WriteLine(_divider);
            Console.WriteLine("#1: Return a list of members who is Male");
            Console.WriteLine("#2: Return the oldest one based on Age");
            Console.WriteLine("#3: Return a new list that contains Full Name only");
            Console.WriteLine("#4: Return lists of members who has birth year is less than/equal to/greater than 2000");
            Console.WriteLine("#5: Return the first person who was born in Ha Noi");
            Console.WriteLine("\n#0: Exit");
            Console.WriteLine(_divider);
            Console.Write("Insert option number: ");

            var optionInput = Console.ReadLine();

            if (int.TryParse(optionInput, out int option))
            {
                switch (option)
                {
                    case 0:
                        isExit = true;
                        break;

                    case 1:
                        GetMembersByGender(Gender.Male);
                        break;

                    case 2:
                        GetOldestMember();
                        break;

                    case 3:
                        GetAllFullNames();
                        break;

                    case 4:
                        GetMembersGroupByBirthYear(
                            out List<Member> birthYearLessThan,
                            out List<Member> birthYearEqualTo,
                            out List<Member> birthYearGreaterThan
                        );
                        break;

                    case 5:
                        GetFirstMemberBornIn("Ha Noi");
                        break;

                    default:
                        Console.WriteLine("\nInvalid option!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input!");
            }

            Console.WriteLine("\nPress any key to back to menu ...");
            Console.ReadKey();
        }

        Console.WriteLine("\nPress any key to exit ...");
        Console.ReadKey();
    }

    private static List<Member> GetMembersByGender(Gender gender)
    {
        Console.Clear();
        Console.WriteLine($"Class member with gender is {gender}:\n" + _divider);

        List<Member> results = _classMembers
            .Where(member => member.Gender == gender)
            .ToList();

        results.ForEach(member => member.PrintInfo());

        return results;
    }

    private static Member GetOldestMember()
    {
        Console.Clear();
        Console.WriteLine("Oldest member in class:\n" + _divider);

        int maxAge = _classMembers.Max(member => member.Age);

        Member oldestMember = _classMembers
            .First(member => member.Age == maxAge);

        oldestMember.PrintInfo();

        return oldestMember;
    }

    private static List<string> GetAllFullNames()
    {
        Console.Clear();
        Console.WriteLine("Class member full names:\n" + _divider);

        List<string> fullNames = _classMembers
            .ConvertAll(member => member.FullName);

        fullNames.ForEach(name => Console.WriteLine(name));
        Console.WriteLine("\n" + _divider);

        return fullNames;
    }

    private static void GetMembersGroupByBirthYear(
        out List<Member> birthYearLessThan,
        out List<Member> birthYearEqualTo,
        out List<Member> birthYearGreaterThan
    )
    {
        Console.Clear();

        birthYearLessThan = _classMembers
            .Where(member => member.DateOfBirth.Year < 2000)
            .ToList();
        birthYearEqualTo = _classMembers
            .Where(member => member.DateOfBirth.Year == 2000)
            .ToList();
        birthYearGreaterThan = _classMembers
            .Where(member => member.DateOfBirth.Year > 2000)
            .ToList();

        Console.WriteLine("Birth year less than 2000:\n" + _divider);
        birthYearLessThan.ForEach(member => member.PrintInfo());

        Console.WriteLine("\nBirth year equal to 2000:\n" + _divider);
        birthYearEqualTo.ForEach(member => member.PrintInfo());

        Console.WriteLine("\nBirth year greater than 2000:\n" + _divider);
        birthYearGreaterThan.ForEach(member => member.PrintInfo());
    }

    private static Member GetFirstMemberBornIn(string birthPlace)
    {
        Console.Clear();
        Console.WriteLine($"First member born in {birthPlace}:\n" + _divider);

        Member? firstMember = _classMembers.Find(member => string.Equals(
            member.BirthPlace, birthPlace.Trim(), StringComparison.OrdinalIgnoreCase));

        if (firstMember != null)
        {
            firstMember.PrintInfo();

            return firstMember;
        }
        else
        {
            Console.WriteLine($"Not found member born in {birthPlace}!\n" + _divider);

            return new Member();
        }
    }
}