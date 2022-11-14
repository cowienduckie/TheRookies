using Common.Enums;

namespace BookLibrary.Data.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Role Role { get; set; }
    public ICollection<BorrowRequest> RequestedBorrowRequests { get; set; } = null!;
    public ICollection<BorrowRequest> ApprovedBorrowRequests { get; set; } = null!;
}