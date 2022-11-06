using BookLibrary.Data.Entities;
using System.Linq.Expressions;

namespace BookLibrary.Data.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetSingleAsync(Expression<Func<User, bool>> predicate);
}