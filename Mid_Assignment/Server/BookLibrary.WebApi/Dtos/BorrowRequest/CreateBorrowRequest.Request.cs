using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class CreateBorrowRequestRequest
{
    [Required] public List<int> BookIds { get; set; } = null!;
}