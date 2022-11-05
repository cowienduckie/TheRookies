using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class CategoryService : ICategoryService
{
    public IEnumerable<GetCategoryResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public GetCategoryResponse? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public CreateCategoryResponse? Create(CreateCategoryRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public UpdateCategoryResponse? Update(UpdateCategoryRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}