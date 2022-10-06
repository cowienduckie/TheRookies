using ClockApp.Events;
using ClockApp.Views;

namespace ClockApp;

public static class Program
{
    public static void Main(string[] args)
    {
        var clock = new Clock();
        var clockDisplay = new ClockDisplay();

        clockDisplay.Subscribe(clock);
        clock.Run();
    }
}
