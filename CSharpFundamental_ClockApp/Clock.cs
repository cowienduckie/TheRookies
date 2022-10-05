namespace ClockApp;

public class Clock
{
    public delegate void ShowTimeHandler(DateTime currentDateTime);
    public event ShowTimeHandler ShowTimeEvent;

    public void OnShowTimeEvent(DateTime currentDateTime)
    {
        ShowTimeEvent?.Invoke(currentDateTime);
    }
}