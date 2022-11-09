using BookLibrary.WebApi.Attributes;
using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Dtos.User;
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
[Route("api/borrow-requests")]
public class BorrowRequestsController : BaseController
{
    private readonly IBorrowRequestService _borrowRequestService;

    public BorrowRequestsController(IBorrowRequestService borrowRequestService)
    {
        _borrowRequestService = borrowRequestService;
    }

    [HttpGet]
    [Authorize(Role.NormalUser, Role.SuperUser)]
    public async Task<ActionResult<IPagedList<GetBorrowRequestResponse>>> GetAll(
        [FromQuery] PagingFilter pagingFilter,
        [FromQuery] SortFilter sortFilter)
    {
        if (CurrentUser == null) return Unauthorized();

        var request = new GetBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = CurrentUser.Id,
                Role = CurrentUser.Role
            }
        };

        try
        {
            var result = await _borrowRequestService.GetAllAsync(request, pagingFilter, sortFilter);

            return Ok(result.ToObject());
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Role.NormalUser, Role.SuperUser)]
    public async Task<ActionResult<GetBorrowRequestResponse>> GetById(int id)
    {
        if (CurrentUser == null) return Unauthorized();

        var request = new GetBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = CurrentUser.Id,
                Role = CurrentUser.Role
            },
            Id = id
        };

        try
        {
            var result = await _borrowRequestService.GetByIdAsync(request);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    [Authorize(Role.NormalUser)]
    public async Task<ActionResult<CreateBorrowRequestResponse>> Create(
        [FromBody] CreateBorrowRequestRequest requestModel)
    {
        if (CurrentUser == null ||
            CurrentUser.Role == Role.SuperUser)
            return Unauthorized();

        requestModel.Requester = new UserModel
        {
            Id = CurrentUser.Id,
            Role = CurrentUser.Role
        };

        try
        {
            var limitCheckMessage =
                await _borrowRequestService.CheckRequestLimit(requestModel);

            if (!string.IsNullOrEmpty(limitCheckMessage)) return BadRequest(limitCheckMessage);

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
    [Authorize(Role.SuperUser)]
    public async Task<ActionResult<ApproveBorrowRequestResponse>> Approve(
        [FromBody] ApproveBorrowRequestRequest requestModel)
    {
        if (CurrentUser == null ||
            CurrentUser.Role == Role.NormalUser)
            return Unauthorized();

        requestModel.Approver = new UserModel
        {
            Id = CurrentUser.Id,
            Role = CurrentUser.Role
        };

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