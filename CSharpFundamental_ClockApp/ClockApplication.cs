namespace ClockApp;

public static class ClockApplication
{
    private const string _divider = "-------------------------------------------------\n";
    private static readonly Clock _clock = new();

    public static void RunApplication()
    {
        _clock.ShowTimeEvent += ShowLocalTimeHandler;
        _clock.ShowTimeEvent += ShowUtcTimeHandler;

        bool isExit = false;

        while (!isExit)
        {
            Console.Clear();
            Console.WriteLine("C# Fundamentals Assignment #3: Clock Application");
            Console.Write(_divider);

            PublishShowCurrentDateTimeEvents();

            isExit = IsExitOrUpdateTime();
        }
    }

    private static bool IsExitOrUpdateTime()
    {
        Console.Write("\nPress any key to exit ...");

        if (Console.KeyAvailable)
        {
            Console.ReadKey();

            return true;
        }
        else
        {
            Thread.Sleep(1000);

            return false;
        }
    }

    private static void PublishShowCurrentDateTimeEvents()
    {
        var currentDateTime = DateTime.Now;

        _clock.OnShowTimeEvent(currentDateTime);
    }

    private static void ShowLocalTimeHandler(DateTime currentDateTime)
    {
        currentDateTime = currentDateTime.ToLocalTime();

        Console.Write("\nLocal time: ");
        Console.WriteLine(currentDateTime);
    }

    private static void ShowUtcTimeHandler(DateTime currentDateTime)
    {
        currentDateTime = currentDateTime.ToUniversalTime();

        Console.Write("\nUTC time: ");
        Console.WriteLine(currentDateTime);
    }
}