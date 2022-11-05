using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class BookLibraryContext : DbContext
{
    public BookLibraryContext(DbContextOptions<BookLibraryContext> options) : base(options)
    {
    }
}