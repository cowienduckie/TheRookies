using ProductStore.Data.Entities;
using ProductStore.Dtos;
using ProductStore.Repositories;

namespace ProductStore.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Product> _productRepository;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = _unitOfWork.GetRepository<Product>();
    }

    public AddProductResponse? Create(AddProductRequest requestModel)
    {
        var newEntity = new Product
        {
            Name = requestModel.Name,
            Manufacture = requestModel.Manufacture,
            CategoryId = requestModel.CategoryId
        };

        var createdEntity = _productRepository.Create(newEntity);

        return _unitOfWork.SaveChanges() > 0 ?
            new AddProductResponse
            {
                Id = createdEntity.Id,
                Name = createdEntity.Name
            }
            : null;
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