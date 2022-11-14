using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Filters;
using Common.DataType;
using Common.Wrappers;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    Task<IPagedList<GetBorrowRequestResponse>> GetPagedListAsync(
        GetBorrowRequestRequest request, PagingFilter pagingFilter, SortFilter sortFilter);

    Task<GetBorrowRequestResponse?> GetByIdAsync(GetBorrowRequestRequest request);

    Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel);

    Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel);

    bool IsExist(int id);

    Task<ValidCheckingWrapper> IsRequestValid(CreateBorrowRequestRequest request);
}