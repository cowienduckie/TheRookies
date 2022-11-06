using BookLibrary.WebApi.Attributes;
using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/borrow-requests")]
public class BorrowRequestsController : BaseController
{
    private readonly IBorrowRequestService _borrowRequestService;

    public BorrowRequestsController(IBorrowRequestService borrowRequestService)
    {
        _borrowRequestService = borrowRequestService;
    }

    [HttpGet]
    [Authorize(Roles.NormalUser, Roles.SuperUser)]
    public async Task<ActionResult<IEnumerable<GetBorrowRequestResponse>>> GetAll()
    {
        try
        {
            var results = await _borrowRequestService.GetAllAsync();

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
    public async Task<ActionResult<GetBorrowRequestResponse>> GetById(int id)
    {
        try
        {
            var result = await _borrowRequestService.GetByIdAsync(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    [Authorize(Roles.NormalUser)]
    public async Task<ActionResult<CreateBorrowRequestResponse>> Create([FromBody] CreateBorrowRequestRequest requestModel)
    {
        if (requestModel == null) return BadRequest();

        try
        {
            var limitCheckMessage = await _borrowRequestService.CheckRequestLimit(1, requestModel);  // TODO: Pass current userId here

            if (!string.IsNullOrEmpty(limitCheckMessage))
            {
                return BadRequest(limitCheckMessage);
            }

            var result = await _borrowRequestService.CreateAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.CreateError);

            return CreatedAtRoute(new {id = result.Id.ToString()}, result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost("approval")]
    [Authorize(Roles.SuperUser)]
    public async Task<ActionResult<ApproveBorrowRequestResponse>> Approve([FromBody] ApproveBorrowRequestRequest requestModel)
    {
        if (requestModel == null) return BadRequest();

        var isExist = _borrowRequestService.IsExist(requestModel.Id);

        if (!isExist) return NotFound();

        try
        {
            var result = await _borrowRequestService.ApproveAsync(requestModel);

            if (result == null) return StatusCode(500, ErrorMessages.UpdateError);

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }
}