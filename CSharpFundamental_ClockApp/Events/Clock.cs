namespace ClockApp.Events;

public class Clock
{
    public delegate void ShowTimeEventHandler(object clock, ShowTimeEventArgs args);
    public event ShowTimeEventHandler? ShowTime;
    private const string _divider = "-------------------------------------------------\n";

    public void Run()
    {
        while (!Console.KeyAvailable)
        {
            Console.Clear();
            Console.WriteLine("C# Fundamentals Assignment #3: Clock Application");
            Console.Write(_divider);

            var currentDateTime = DateTime.Now;
            var ShowTimeEventArgs = new ShowTimeEventArgs(currentDateTime);

            OnShowTime(this, ShowTimeEventArgs);

            Console.Write("\nPress any key to exit ...");

            Thread.Sleep(1000);
        }
    }

    protected void OnShowTime(object clock, ShowTimeEventArgs args)
    {
        ShowTime?.Invoke(clock, args);
    }
}
