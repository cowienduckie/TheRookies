namespace ClockApp.Events;

public class ShowTimeEventArgs : EventArgs
{
    public readonly DateTime currentDateTime;

    public ShowTimeEventArgs(DateTime currentDateTime)
    {
        this.currentDateTime = currentDateTime;
    }
}