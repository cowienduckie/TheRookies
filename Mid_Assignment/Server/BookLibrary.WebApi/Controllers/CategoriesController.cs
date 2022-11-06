using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCategoryResponse>>> GetAll()
    {
        try
        {
            var results = await _categoryService.GetAllAsync();

            if (!results.Any()) return NotFound();

            return Ok(results);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCategoryResponse>> GetById(int id)
    {
        try
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateCategoryResponse>> Create([FromBody] CreateCategoryRequest requestModel)
    {
        if (requestModel == null) return BadRequest();

        try
        {
            var result = await _categoryService.CreateAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.CreateError);

            return CreatedAtRoute(new {id = result.Id.ToString()}, result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateCategoryResponse>> Update([FromBody] UpdateCategoryRequest requestModel)
    {
        if (requestModel == null) return BadRequest();

        var isExist = _categoryService.IsExist(requestModel.Id);

        if (!isExist) return NotFound();

        try
        {
            var result = await _categoryService.UpdateAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.UpdateError);

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var isExist = _categoryService.IsExist(id);

        if (!isExist) return NotFound();

        try
        {
            var result = await _categoryService.Delete(id);

            if (!result) return StatusCode(500, ErrorMessages.DeleteError);

            return NoContent();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }
}