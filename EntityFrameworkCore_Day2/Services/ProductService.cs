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
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var category = _categoryRepository.Get(c => c.Id == requestModel.CategoryId);

            if (category == null)
            {
                return null;
            }

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
        catch
        {
            transaction.Rollback();

            return null;
        }
    }

    public bool Delete(int id)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var entity = _productRepository.Get(entity => entity.Id == id);

            if (entity == null)
            {
                return false;
            }

            _productRepository.Delete(entity);

            _unitOfWork.SaveChanges();

            transaction.Commit();

            return true;
        }
        catch
        {
            transaction.Rollback();

            return false;
        }
    }

    public IEnumerable<GetProductResponse> GetAll()
    {
        return _productRepository
            .GetAll(null)
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

        if (entity == null)
        {
            return null;
        }

        return new GetProductResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Manufacture = entity.Manufacture,
            CategoryId = entity.CategoryId
        };
    }

    public UpdateProductResponse? Update(int id, UpdateProductRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var entity = _productRepository.Get(entity => entity.Id == id);

            if (entity == null)
            {
                return null;
            }

            var category = _categoryRepository.Get(c => c.Id == requestModel.CategoryId);

            if (category == null)
            {
                return null;
            }

            entity.Name = requestModel.Name;
            entity.Manufacture = requestModel.Manufacture;
            entity.CategoryId = requestModel.CategoryId;

            var updatedEntity = _productRepository.Update(entity);

            _unitOfWork.SaveChanges();

            transaction.Commit();

            return new UpdateProductResponse
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name,
                Manufacture = entity.Manufacture,
                CategoryId = entity.CategoryId
            };
        }
        catch
        {
            transaction.Rollback();

            return null;
        }
    }
}