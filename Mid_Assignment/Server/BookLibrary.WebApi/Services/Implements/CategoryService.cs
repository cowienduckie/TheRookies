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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest requestModel)
    {
        var newCategory = new Category
        {
            Name = requestModel.Name
        };

        var createdCategory = _categoryRepository.Create(newCategory);

        await _unitOfWork.SaveChangesAsync();

        return new CreateCategoryResponse
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name
        };
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _categoryRepository.GetSingleAsync(category => category.Id == id);

        if (category == null) return false;

        _categoryRepository.Delete(category);

        await _unitOfWork.SaveChangesAsync();

        return true;
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
        var category = await _categoryRepository.GetSingleAsync(category => category.Id == id);

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
        var category = await _categoryRepository.GetSingleAsync(category => category.Id == requestModel.Id);

        if (category == null)
        {
            return null;
        }

        category.Name = requestModel.Name;

        var updatedCategory = _categoryRepository.Update(category);

        await _unitOfWork.SaveChangesAsync();

        return new UpdateCategoryResponse
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };
    }
}