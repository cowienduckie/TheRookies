using Microsoft.AspNetCore.Mvc;
using ProductStore.Dtos;
using ProductStore.Services;

namespace ProductStore.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IEnumerable<GetProductResponse> GetAll()
    {
        return _productService.GetAll();
    }

    [HttpGet("{id}")]
    public GetProductResponse? GetById(int id)
    {
        return _productService.GetById(id);
    }

    [HttpPost]
    public AddProductResponse? Add([FromBody] AddProductRequest requestModel)
    {
        return _productService.Create(requestModel);
    }

    [HttpPut("{id}")]
    public UpdateProductResponse? Update(int id, [FromBody] UpdateProductRequest requestModel)
    {
        return _productService.Update(id, requestModel);
    }

    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _productService.Delete(id);
    }
}