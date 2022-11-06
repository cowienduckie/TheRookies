using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(BookLibraryContext context)
    {
        _dbSet = context.Set<T>();
    }

    public T Create(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet.FirstOrDefaultAsync() : _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

        return await dbSet.ToListAsync();
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }

    public bool IsExist(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }
}