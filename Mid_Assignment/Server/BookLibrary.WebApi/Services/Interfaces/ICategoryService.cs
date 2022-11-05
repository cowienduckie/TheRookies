using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface ICategoryService
{
    IEnumerable<GetCategoryResponse> GetAll();
    GetCategoryResponse? GetById(int id);
    CreateCategoryResponse? Create(CreateCategoryRequest requestModel);
    UpdateCategoryResponse? Update(UpdateCategoryRequest requestModel);
    bool Delete(int id);
}