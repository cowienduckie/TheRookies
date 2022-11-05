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

    public T? Get(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet.FirstOrDefault() : _dbSet.FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet : _dbSet.Where(predicate);
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;
    }
}