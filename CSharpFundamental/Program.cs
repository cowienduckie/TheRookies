namespace CSharpFundamental;
public static class Program
{
    public static void Main(string[] args)
    {
        /*
            #1: Return a list of members who isMale
        */
        Solution.GetMembersByGender(Gender.Male);

        /*
            #2: Return the oldest one based on “Age” 
                If return more than one record then select first record
                If you can get age fromDate Of Birth to compare then it would be better
        */
        Solution.GetOldestMember();

        /*
            #3: Return a new list that contains FullName only 
                FullName = LastName + FirstName
        */
        Solution.GetAllFullNames();

        /*
            #4: Return 3 lists:
            - List of members who has birth year is 2000
            - List of members who has birth year greater than 2000
            - List of members who has birth year less than 2000
        */
        Solution.GetMembersGroupByBirthYear(
            out List<ClassMember> birthYearLessThan,
            out List<ClassMember> birthYearEqualTo,
            out List<ClassMember> birthYearGreaterThan,
            2000
        );

        /*
            #5: Return the first person who was born in Ha Noi.
        */
        Solution.GetFirstMemberBornIn("Hanoi");
    }
}
