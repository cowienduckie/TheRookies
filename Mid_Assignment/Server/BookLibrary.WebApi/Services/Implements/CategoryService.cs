using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class CategoryService : ICategoryService
{
    public Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetCategoryResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public GetCategoryResponse? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateCategoryResponse?> UpdateAsync(UpdateCategoryRequest requestModel)
    {
        throw new NotImplementedException();
    }
}