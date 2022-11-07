using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Interfaces;
using Common.DataType;
using Common.Enums;

namespace BookLibrary.WebApi.Services.Implements;

public class CategoryService : ICategoryService
{
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = _unitOfWork.GetRepository<Category>();
    }

    public async Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var newCategory = new Category
            {
                Name = requestModel.Name
            };

            var createdCategory = _categoryRepository.Create(newCategory);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new CreateCategoryResponse
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var category = await _categoryRepository.GetAsync(category => category.Id == id);

            if (category == null) return false;

            _categoryRepository.Delete(category);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return false;
        }
    }

    public async Task<IPagedList<GetCategoryResponse>> GetAllAsync(PagingFilter pagingFilter, SortFilter sortFilter)
    {
        var categories = (await _categoryRepository.GetAllAsync()).AsQueryable();

        var validSortFields = new[]
        {
            SortField.Id,
            SortField.Name
        };

        var sortedCategories = categories
            .SortData(validSortFields, sortFilter.SortField, sortFilter.SortOrder)
            .Select(category => new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            })
            .AsQueryable();

        return new PagedList<GetCategoryResponse>
            (sortedCategories, pagingFilter.PageIndex, pagingFilter.PageSize);
    }

    public async Task<GetCategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetAsync(category => category.Id == id);

        if (category == null) return null;

        return new GetCategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public bool IsExist(int id)
    {
        return _categoryRepository.IsExist(category => category.Id == id);
    }

    public async Task<UpdateCategoryResponse?> UpdateAsync(UpdateCategoryRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var category = await _categoryRepository.GetAsync(category => category.Id == requestModel.Id);

            if (category == null) return null;

            category.Name = requestModel.Name;

            var updatedCategory = _categoryRepository.Update(category);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new UpdateCategoryResponse
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }
}