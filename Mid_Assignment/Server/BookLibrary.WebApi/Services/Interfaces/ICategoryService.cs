using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategoryResponse>> GetAllAsync();
    Task<GetCategoryResponse?> GetByIdAsync(int id);
    Task<CreateCategoryResponse?> CreateAsync(CreateCategoryRequest requestModel);
    Task<UpdateCategoryResponse?> UpdateAsync(UpdateCategoryRequest requestModel);
    Task<bool> Delete(int id);
    bool IsExist(int id);
}