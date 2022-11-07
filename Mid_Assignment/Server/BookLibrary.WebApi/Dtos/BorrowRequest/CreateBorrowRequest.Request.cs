using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookLibrary.WebApi.Dtos.User;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class CreateBorrowRequestRequest
{
    [Required] public List<int> BookIds { get; set; } = null!;

    [JsonIgnore]
    public UserModel Requester { get; set; } = null!;
}