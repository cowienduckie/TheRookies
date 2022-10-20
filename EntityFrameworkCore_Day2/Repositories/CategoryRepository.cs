using ProductStore.Data;
using ProductStore.Data.Entities;

namespace ProductStore.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ProductStoreContext context) : base(context)
    {
    }
}