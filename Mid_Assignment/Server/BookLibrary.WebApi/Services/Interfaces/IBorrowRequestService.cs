using BookLibrary.WebApi.Dtos.BorrowRequest;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    Task<IEnumerable<GetBorrowRequestResponse>> GetAllAsync();
    Task<GetBorrowRequestResponse?> GetByIdAsync(int id);
    Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel);
    Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel);
    bool IsExist(int id);
    Task<string> CheckRequestLimit(int userId, CreateBorrowRequestRequest request);
}