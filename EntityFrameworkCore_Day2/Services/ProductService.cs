using ProductStore.Data.Entities;
using ProductStore.Dtos;
using ProductStore.Repositories;

namespace ProductStore.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Category> _categoryRepository;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = _unitOfWork.GetRepository<Product>();
        _categoryRepository = _unitOfWork.GetRepository<Category>();
    }

    public AddProductResponse? Create(AddProductRequest requestModel)
    {
        using var transaction = _productRepository.DataBaseTransaction();

        try
        {
            var category = _categoryRepository.Get(c => c.Id == requestModel.CategoryId);

            if (category != null)
            {
                var newEntity = new Product
                {
                    Name = requestModel.Name,
                    Manufacture = requestModel.Manufacture,
                    CategoryId = requestModel.CategoryId
                };

                var createdEntity = _productRepository.Create(newEntity);

                _unitOfWork.SaveChanges();

                transaction.Commit();

                return new AddProductResponse
                {
                    Id = createdEntity.Id,
                    Name = createdEntity.Name
                };
            }

            return null;
        }
        catch
        {
            transaction.Rollback();

            return null;
        }
    }

    public bool Delete(int id)
    {
        var entity = _productRepository.Get(entity => entity.Id == id);

        if (entity == null) return false;

        bool isSucceeded = _productRepository.Delete(entity);

        isSucceeded &= _unitOfWork.SaveChanges() > 0;

        return isSucceeded;
    }

    public IEnumerable<GetProductResponse> GetAll()
    {
        return _productRepository
            .GetAll(_ => true)
            .Select(entity => new GetProductResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Manufacture = entity.Manufacture,
                CategoryId = entity.CategoryId
            });
    }

    public GetProductResponse? GetById(int id)
    {
        var entity = _productRepository.Get(entity => entity.Id == id);

        return entity != null ?
            new GetProductResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Manufacture = entity.Manufacture,
                CategoryId = entity.CategoryId
            }
            : null;
    }

    public UpdateProductResponse? Update(int id, UpdateProductRequest requestModel)
    {
        var entity = _productRepository.Get(entity => entity.Id == id);

        if (entity == null) return null;

        entity.Name = requestModel.Name;
        entity.Manufacture = requestModel.Manufacture;
        entity.CategoryId = requestModel.CategoryId;

        var updatedEntity = _productRepository.Update(entity);

        return _unitOfWork.SaveChanges() > 0 ?
            new UpdateProductResponse
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name,
                Manufacture = entity.Manufacture,
                CategoryId = entity.CategoryId
            }
            : null;
    }
}