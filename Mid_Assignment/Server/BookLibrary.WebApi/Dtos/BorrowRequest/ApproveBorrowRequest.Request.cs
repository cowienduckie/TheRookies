using BookLibrary.WebApi.Dtos.User;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class ApproveBorrowRequestRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public bool IsApproved { get; set; }

    [JsonIgnore]
    public UserModel? Approver { get; set; }
}