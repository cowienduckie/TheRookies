using ProductStore.Dtos;

namespace ProductStore.Services;

public interface ICategoryService
{
    IEnumerable<GetCategoryResponse> GetAll();
    GetCategoryResponse? GetById(int id);
    AddCategoryResponse? Create(AddCategoryRequest requestModel);
    UpdateCategoryResponse? Update(int id, UpdateCategoryRequest requestModel);
    bool Delete(int id);
}