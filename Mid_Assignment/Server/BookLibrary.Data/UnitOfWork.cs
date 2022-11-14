using BookLibrary.Data.Interfaces;

namespace BookLibrary.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookLibraryContext _context;

    public UnitOfWork(BookLibraryContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}