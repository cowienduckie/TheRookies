using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(BookLibraryContext context)
        : base(context)
    {
    }

    public override async Task<IEnumerable<Book>> GetAllAsync(Expression<Func<Book, bool>>? predicate = null)
    {
        var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

        return await dbSet
            .Include(book => book.Categories)
            .ToListAsync();
    }

    public override async Task<Book?> GetAsync(Expression<Func<Book, bool>>? predicate = null)
    {
        var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

        return await dbSet
            .Include(book => book.Categories)
            .FirstOrDefaultAsync();
    }
}