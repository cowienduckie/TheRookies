using ProductStore.Data.Entities;
using ProductStore.Repositories;

namespace ProductStore.Repositories;

public interface IUnitOfWork
{
    BaseRepository<T> GetRepository<T>() where T : BaseEntity<int>;
    int SaveChanges();
}