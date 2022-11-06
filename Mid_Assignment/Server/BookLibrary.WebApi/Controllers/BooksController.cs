using BookLibrary.WebApi.Attributes;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/books")]
public class BooksController : BaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [Authorize(Roles.NormalUser, Roles.SuperUser)]
    public async Task<ActionResult<IEnumerable<GetBookResponse>>> GetAll()
    {
        try
        {
            var results = await _bookService.GetAllAsync();

            if (!results.Any()) return NotFound();

            return Ok(results);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles.NormalUser, Roles.SuperUser)]
    public async Task<ActionResult<GetBookResponse>> GetById(int id)
    {
        try
        {
            var result = await _bookService.GetByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    [Authorize(Roles.SuperUser)]
    public async Task<ActionResult<CreateBookResponse>> Create([FromBody] CreateBookRequest requestModel)
    {
        try
        {
            var result = await _bookService.CreateAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.CreateError);

            return CreatedAtRoute(new {id = result.Id.ToString()}, result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPut]
    [Authorize(Roles.SuperUser)]
    public async Task<ActionResult<UpdateBookResponse>> Update([FromBody] UpdateBookRequest requestModel)
    {
        var isExist = _bookService.IsExist(requestModel.Id);

        if (!isExist) return NotFound();

        try
        {
            var result = await _bookService.UpdateAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.UpdateError);

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.SuperUser)]
    public async Task<IActionResult> Delete(int id)
    {
        var isExist = _bookService.IsExist(id);

        if (!isExist) return NotFound();

        try
        {
            var result = await _bookService.Delete(id);

            if (!result) return StatusCode(500, ErrorMessages.DeleteError);

            return NoContent();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }
}