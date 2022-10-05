namespace PrimeNumberApp;

public static class PrimeNumberApp
{
    private const string _divider = "-------------------------------------------------------\n";

    public static async Task RunApplicationAsync()
    {
        bool isExit = false;

        while (!isExit)
        {
            Console.Clear();
            Console.WriteLine("C# Fundamentals Assignment #3: Prime Number Application");
            Console.WriteLine(_divider);

            int leftBound = GetInputNumber("Insert left bound: ");
            int rightBound = GetInputNumber("Insert right bound: ");

            var primeNumbers = await GetPrimeNumbersAsync(leftBound, rightBound);

            if (primeNumbers.Any())
            {
                Console.WriteLine(_divider);
                Console.Write($"Prime number(s) from {leftBound} to {rightBound}: ");

                primeNumbers.ForEach(primeNumber => Console.Write(primeNumber + " "));

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(_divider);
                Console.WriteLine($"Prime number(s) from {leftBound} to {rightBound}: There is no prime number!");
            }

            Console.Write("\n\nPress Esc key to exit or another key to continue ...");

            var pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.Escape)
            {
                isExit = true;
            }
        }
    }

    private static int GetInputNumber(string inputMessage)
    {
        int outputNumber = 0;
        bool isInputValid = false;

        while (!isInputValid)
        {
            Console.Write(inputMessage);

            var inputNumber = Console.ReadLine();
            isInputValid = int.TryParse(inputNumber, out int number);

            if (isInputValid)
            {
                outputNumber = number;
            }
            else
            {
                Console.WriteLine("\nInvalid input!\n");
            }
        }

        Console.WriteLine();

        return outputNumber;
    }

    private static async Task<List<int>> GetPrimeNumbersAsync(int leftBound, int rightBound)
    {
        const int smallestPrimeNumber = 2;

        var primeNumbers = new List<int>();

        if (leftBound > rightBound || rightBound < smallestPrimeNumber)
        {
            return primeNumbers;
        }

        leftBound = Math.Max(leftBound, smallestPrimeNumber);

        var findPrimeTasks = SplitFindPrimeTasksIntoChunks(leftBound, rightBound);

        var taskResults = await Task.WhenAll(findPrimeTasks);

        if (taskResults != null)
        {
            foreach (List<int> result in taskResults)
            {
                primeNumbers.AddRange(result);
            }
        }

        return primeNumbers;
    }

    private static List<Task<List<int>>> SplitFindPrimeTasksIntoChunks(int leftBound, int rightBound)
    {
        const int chunkSize = 10;

        var findPrimeTasks = new List<Task<List<int>>>();

        for (int currentNumber = leftBound; currentNumber <= rightBound; currentNumber += chunkSize)
        {
            int fromNumber = currentNumber;
            int toNumber = Math.Min(rightBound, fromNumber + chunkSize - 1);

            var findPrimeTask = Task.Run(() => FindPrimeNumbersInRange(fromNumber, toNumber));

            findPrimeTasks.Add(findPrimeTask);
        }

        return findPrimeTasks;
    }

    private static List<int> FindPrimeNumbersInRange(int fromNumber, int toNumber)
    {
        var primeNumbers = new List<int>();

        for (int currentNumber = fromNumber; currentNumber <= toNumber; ++currentNumber)
        {
            bool isPrimeNumber = IsPrimeNumber(currentNumber);

            if (isPrimeNumber)
            {
                primeNumbers.Add(currentNumber);
            }
        }

        return primeNumbers;
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