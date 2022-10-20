using ProductStore.Data;
using ProductStore.Data.Entities;

namespace ProductStore.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ProductStoreContext context) : base(context)
    {
    }
}