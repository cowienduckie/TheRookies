using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApi.Models;

public class TaskCreateModel
{
    [Required]
    public string Title { get; set; } = string.Empty;
}