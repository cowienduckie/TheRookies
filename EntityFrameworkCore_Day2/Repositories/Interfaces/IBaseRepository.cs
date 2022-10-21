using System.Linq.Expressions;
using ProductStore.Data;
using ProductStore.Data.Entities;

namespace ProductStore.Repositories;

public interface IBaseRepository<T> where T : BaseEntity<int>
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate);
    T? Get(Expression<Func<T, bool>>? predicate);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
}