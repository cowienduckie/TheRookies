using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApi.Models;

public class TaskUpdateModel
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public bool IsCompleted { get; set; }
}