namespace AspNetCoreApi.Models;

public class TaskModel
{
    public Guid Id { get; }
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }

    public TaskModel(string? title, bool isCompleted)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = isCompleted;
    }
}