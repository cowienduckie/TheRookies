namespace Common.Wrappers;

public class ValidCheckingWrapper
{
    public ValidCheckingWrapper(string? message = null)
    {
        if (string.IsNullOrEmpty(message))
        {
            IsValid = true;
            Message = null;
        }
        else
        {
            IsValid = false;
            Message = message;
        }
    }

    public bool IsValid { get; set; }
    public string? Message { get; set; }
}