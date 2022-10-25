using ProductStore.Dtos;

namespace ProductStore.Services;

public interface IProductService
{
    IEnumerable<GetProductResponse> GetAll();
    GetProductResponse? GetById(int id);
    AddProductResponse? Create(AddProductRequest requestModel);
    UpdateProductResponse? Update(UpdateProductRequest requestModel);
    bool Delete(int id);
}