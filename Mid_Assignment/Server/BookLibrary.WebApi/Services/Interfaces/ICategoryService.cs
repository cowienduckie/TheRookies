using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategoryResponse>> GetAllAsync();
    GetCategoryResponse? GetById(int id);
    Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest requestModel);
    Task<UpdateCategoryResponse?> UpdateAsync(UpdateCategoryRequest requestModel);
    bool Delete(int id);
}