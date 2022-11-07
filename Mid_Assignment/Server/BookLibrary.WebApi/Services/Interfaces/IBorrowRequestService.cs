using BookLibrary.WebApi.Dtos.BorrowRequest;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    Task<IEnumerable<GetBorrowRequestResponse>> GetAllAsync(GetBorrowRequestRequest request);
    Task<GetBorrowRequestResponse?> GetByIdAsync(GetBorrowRequestRequest request);
    Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel);
    Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel);
    bool IsExist(int id);
    Task<string> CheckRequestLimit(CreateBorrowRequestRequest request);
}