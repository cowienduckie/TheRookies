using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;

namespace BookLibrary.Data.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BookLibraryContext context)
        : base(context)
    {
    }
}