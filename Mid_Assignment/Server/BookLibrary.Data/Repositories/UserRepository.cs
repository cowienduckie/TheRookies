using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(BookLibraryContext context)
        : base(context)
    {
    }

    public async Task<User?> GetSingleAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }
}