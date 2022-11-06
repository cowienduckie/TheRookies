using System.ComponentModel.DataAnnotations;

namespace BookLibrary.WebApi.Dtos.BorrowRequest;

public class ApproveBorrowRequestRequest
{
    [Required] public int Id { get; set; }

    [Required] public bool IsApproved { get; set; }
}