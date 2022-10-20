using ProductStore.Data.Entities;
using ProductStore.Dtos;
using ProductStore.Repositories;

namespace ProductStore.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Category> _categoryRepository;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = _unitOfWork.GetRepository<Category>();
    }

    public AddCategoryResponse? Create(AddCategoryRequest requestModel)
    {
        var newEntity = new Category
        {
            Name = requestModel.Name
        };

        var createdEntity = _categoryRepository.Create(newEntity);

        return _unitOfWork.SaveChanges() > 0 ?
            new AddCategoryResponse
            {
                Id = createdEntity.Id,
                Name = createdEntity.Name
            }
            : null;
    }

    public bool Delete(int id)
    {
        var entity = _categoryRepository.Get(entity => entity.Id == id);

        if (entity == null) return false;

        bool isSucceeded = _categoryRepository.Delete(entity);

        isSucceeded &= _unitOfWork.SaveChanges() > 0;

        return isSucceeded;
    }

    public IEnumerable<GetCategoryResponse> GetAll()
    {
        return _categoryRepository
            .GetAll(_ => true)
            .Select(entity => new GetCategoryResponse
            {
                Id = entity.Id,
                Name = entity.Name
            });
    }

    public GetCategoryResponse? GetById(int id)
    {
        var entity = _categoryRepository.Get(entity => entity.Id == id);

        return entity != null ?
            new GetCategoryResponse
            {
                Id = entity.Id,
                Name = entity.Name
            }
            : null;
    }

    public UpdateCategoryResponse? Update(int id, UpdateCategoryRequest requestModel)
    {
        var entity = _categoryRepository.Get(entity => entity.Id == id);

        if (entity == null) return null;

        entity.Name = requestModel.Name;

        var updatedEntity = _categoryRepository.Update(entity);

        return _unitOfWork.SaveChanges() > 0 ?
            new UpdateCategoryResponse
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name
            }
            : null;
    }
}