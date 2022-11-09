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

    public override Task<User?> GetSingleAsync(Expression<Func<User, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet.SingleOrDefaultAsync() : _dbSet.SingleOrDefaultAsync(predicate);
    }
}