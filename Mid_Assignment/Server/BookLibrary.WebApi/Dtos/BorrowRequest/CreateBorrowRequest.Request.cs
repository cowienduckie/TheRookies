using BookLibrary.WebApi.Dtos.User;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class CreateBorrowRequestRequest
{
    [Required]
    public List<int> BookIds { get; set; } = null!;

    [JsonIgnore]
    public UserModel? Requester { get; set; }
}