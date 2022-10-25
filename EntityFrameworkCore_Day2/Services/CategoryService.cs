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
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var newEntity = new Category
            {
                Name = requestModel.Name
            };

            var createdEntity = _categoryRepository.Create(newEntity);

            _unitOfWork.SaveChanges();

            transaction.Commit();

            return new AddCategoryResponse
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
            var entity = _categoryRepository.Get(entity => entity.Id == id);

            if (entity == null)
            {
                return false;
            }

            _categoryRepository.Delete(entity);

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

    public IEnumerable<GetCategoryResponse> GetAll()
    {
        return _categoryRepository
            .GetAll()
            .Select(entity => new GetCategoryResponse
            {
                Id = entity.Id,
                Name = entity.Name
            });
    }

    public GetCategoryResponse? GetById(int id)
    {
        var entity = _categoryRepository.Get(entity => entity.Id == id);

        if (entity == null)
        {
            return null;
        }

        return new GetCategoryResponse
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public UpdateCategoryResponse? Update(UpdateCategoryRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var entity = _categoryRepository.Get(entity => entity.Id == requestModel.Id);

            if (entity == null)
            {
                return null;
            }

            entity.Name = requestModel.Name;

            var updatedEntity = _categoryRepository.Update(entity);

            _unitOfWork.SaveChanges();

            transaction.Commit();

            return new UpdateCategoryResponse
            {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name
            };
        }
        catch
        {
            transaction.Rollback();

            return null;
        }
    }
}