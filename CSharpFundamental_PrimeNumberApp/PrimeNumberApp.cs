using System.Threading.Tasks;
namespace PrimeNumberApp;

public static class PrimeNumberApp
{
    private const string _divider = "-------------------------------------------------------\n";

    public static void Run()
    {
        Console.Clear();
        Console.WriteLine("C# Fundamentals Assignment #3: Prime Number Application");
        Console.WriteLine(_divider);

        const int leftBound = 1;
        const int rightBound = 1000;

        Console.Write($"Prime number(s) from {leftBound} to {rightBound}: ");

        var findPrimeTasks = SplitRangeIntoTasks(leftBound, rightBound).ToArray();

        Task.WaitAll(findPrimeTasks);

        Console.Write("\n\nPress any key to exit ...");
        Console.ReadKey();
    }

    private static List<Task> SplitRangeIntoTasks(int leftBound, int rightBound)
    {
        const int interval = 50;
        const int smallestPrimeNumber = 2;

        var findPrimeTasks = new List<Task>();

        if (leftBound > rightBound || rightBound < smallestPrimeNumber)
        {
            return findPrimeTasks;
        }

        leftBound = Math.Max(leftBound, smallestPrimeNumber);

        for (int currentNumber = leftBound; currentNumber <= rightBound; currentNumber += interval)
        {
            int fromNumber = currentNumber;
            int toNumber = Math.Min(rightBound, fromNumber + interval - 1);

            var findPrimeTask = Task.Run(() => GetPrimeNumbers(fromNumber, toNumber));

            findPrimeTasks.Add(findPrimeTask);
        }

        return findPrimeTasks;
    }

    private static void GetPrimeNumbers(int fromNumber, int toNumber)
    {
        for (int currentNumber = fromNumber; currentNumber <= toNumber; ++currentNumber)
        {
            bool isPrimeNumber = IsPrimeNumber(currentNumber);

            if (isPrimeNumber)
            {
                Console.Write(currentNumber + " ");
            }
        }
    }

    private static bool IsPrimeNumber(int number)
    {
        for (int i = 2; i <= number / 2; ++i)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}