using ClockApp.Events;

namespace ClockApp.Views;

public class ClockDisplay
{
    public void Subscribe(Clock clock)
    {
        clock.ShowTime += ShowLocalTimeHandler;
        clock.ShowTime += ShowUtcTimeHandler;
    }

    private void ShowLocalTimeHandler(object clock, ShowTimeEventArgs args)
    {
        var currentDateTime = args.currentDateTime.ToLocalTime();

        Console.Write("\nLocal time: ");
        Console.WriteLine(currentDateTime.ToString("hh:mm:ss"));
    }

    private void ShowUtcTimeHandler(object clock, ShowTimeEventArgs args)
    {
        var currentDateTime = args.currentDateTime.ToUniversalTime();

        Console.Write("\nUTC time: ");
        Console.WriteLine(currentDateTime.ToString("hh:mm:ss"));
    }
}