using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;

namespace BookLibrary.Data.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(BookLibraryContext context)
    : base(context)
    {
    }
}