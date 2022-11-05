using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;

namespace BookLibrary.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(BookLibraryContext context)
    : base(context)
    {
    }
}