using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;

namespace BookLibrary.Data.Repositories;

public class BorrowRequestRepository : BaseRepository<BorrowRequest>, IBorrowRequestRepository
{
    public BorrowRequestRepository(BookLibraryContext context)
    : base(context)
    {
    }
}