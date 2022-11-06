using System.Linq.Expressions;
using BookLibrary.Data.Entities;

namespace BookLibrary.Data.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetSingleAsync(Expression<Func<User, bool>> predicate);
}