using Common.Constants;

namespace BookLibrary.Data.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Cover { get; set; } = CommonConstants.BaseBookCoverUrl;
    public ICollection<Category> Categories { get; set; } = null!;
    public ICollection<BorrowRequest> BorrowRequests { get; set; } = null!;
}