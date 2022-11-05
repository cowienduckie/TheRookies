using Common.Constants;

namespace BookLibrary.Data.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Cover { get; set; } = CommonConstants.BaseBookCoverUrl;
    public ICollection<BookCategory> BookCategories { get; set; } = null!;
    public ICollection<BorrowRequestDetail> BoorBorrowRequestDetails { get; set; } = null!;
}