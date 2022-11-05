using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.Data.Repositories;

namespace BookLibrary.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookLibraryContext _context;

    public UnitOfWork(BookLibraryContext context)
    {
        _context = context;
    }

    public IDatabaseTransaction GetDatabaseTransaction()
    {
        return new EntityDatabaseTransaction(_context);
    }

    public BaseRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new BaseRepository<T>(_context);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}