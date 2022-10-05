namespace ClockApp;

public class Clock
{
    public delegate void ShowTimeEventHandler(DateTime currentDateTime);
    public event ShowTimeEventHandler ShowTimeEvent;

    public void OnShowTimeEvent(DateTime currentDateTime)
    {
        ShowTimeEvent?.Invoke(currentDateTime);
    }
}