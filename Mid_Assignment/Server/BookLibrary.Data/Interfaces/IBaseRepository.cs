using System.Linq.Expressions;
using BookLibrary.Data.Entities;

namespace BookLibrary.Data.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
    bool IsExist(Expression<Func<T, bool>> predicate);
}