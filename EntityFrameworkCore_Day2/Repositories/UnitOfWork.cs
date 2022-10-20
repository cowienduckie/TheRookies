using ProductStore.Data;
using ProductStore.Data.Entities;

namespace ProductStore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductStoreContext _context;

    public UnitOfWork(ProductStoreContext context)
    {
        _context = context;
    }

    public BaseRepository<T> GetRepository<T>() where T : BaseEntity<int>
    {
        return new BaseRepository<T>(_context);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}