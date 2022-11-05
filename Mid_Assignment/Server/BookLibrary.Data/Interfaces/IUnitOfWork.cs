using BookLibrary.Data.Entities;
using BookLibrary.Data.Repositories;

namespace BookLibrary.Data.Interfaces;

public interface IUnitOfWork
{
    BaseRepository<T> GetRepository<T>() where T : BaseEntity;
    int SaveChanges();
    IDatabaseTransaction GetDatabaseTransaction();
}