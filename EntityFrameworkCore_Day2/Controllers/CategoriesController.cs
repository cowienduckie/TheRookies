using Microsoft.AspNetCore.Mvc;
using ProductStore.Dtos;
using ProductStore.Services;

namespace ProductStore.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IEnumerable<GetCategoryResponse> GetAll()
    {
        return _categoryService.GetAll();
    }

    [HttpGet("{id}")]
    public GetCategoryResponse? GetById(int id)
    {
        return _categoryService.GetById(id);
    }

    [HttpPost]
    public AddCategoryResponse? Add([FromBody] AddCategoryRequest requestModel)
    {
        return _categoryService.Create(requestModel);
    }

    [HttpPut("{id}")]
    public UpdateCategoryResponse? Update(int id, [FromBody] UpdateCategoryRequest requestModel)
    {
        return _categoryService.Update(id, requestModel);
    }

    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _categoryService.Delete(id);
    }
}