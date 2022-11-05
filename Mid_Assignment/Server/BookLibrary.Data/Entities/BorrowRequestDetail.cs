namespace BookLibrary.Data.Entities;

public class BorrowRequestDetail : BaseEntity
{
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int BorrowRequestId { get; set; }
    public BorrowRequest BorrowRequest { get; set; } = null!;
}