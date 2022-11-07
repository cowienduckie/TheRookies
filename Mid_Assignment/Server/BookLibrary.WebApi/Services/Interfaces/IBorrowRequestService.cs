using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Filters;
using Common.DataType;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    Task<IPagedList<GetBorrowRequestResponse>> GetAllAsync(
        GetBorrowRequestRequest request, PagingFilter pagingFilter, SortFilter sortFilter);
    Task<GetBorrowRequestResponse?> GetByIdAsync(GetBorrowRequestRequest request);
    Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel);
    Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel);
    bool IsExist(int id);
    Task<string> CheckRequestLimit(CreateBorrowRequestRequest request);
}