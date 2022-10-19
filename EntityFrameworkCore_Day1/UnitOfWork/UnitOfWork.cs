using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Repositories;

namespace StudentManagement.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly StudentManagementContext _context;

    public UnitOfWork(StudentManagementContext context)
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