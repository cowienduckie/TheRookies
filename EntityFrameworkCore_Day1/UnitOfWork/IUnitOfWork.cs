using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.UnitOfWork;

public interface IUnitOfWork
{
    BaseRepository<T> GetRepository<T>() where T : BaseEntity<int>;
    int SaveChanges();
}