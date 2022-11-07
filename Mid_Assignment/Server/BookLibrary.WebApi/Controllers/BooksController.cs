using BookLibrary.WebApi.Attributes;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.DataType;
using Common.Enums;
using Common.Extensions;
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
    [Authorize(Role.NormalUser, Role.SuperUser)]
    public async Task<ActionResult<IPagedList<GetBookResponse>>> GetAll(
        [FromQuery] PagingFilter pagingFilter,
        [FromQuery] SortFilter sortFilter)
    {
        try
        {
            var result = await _bookService.GetAllAsync(pagingFilter, sortFilter);

            return Ok(result.ToObject());
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Role.NormalUser, Role.SuperUser)]
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
    [Authorize(Role.SuperUser)]
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
    [Authorize(Role.SuperUser)]
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
    [Authorize(Role.SuperUser)]
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