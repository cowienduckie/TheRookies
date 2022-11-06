using BookLibrary.WebApi.Dtos.BorrowRequest;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    Task<IEnumerable<GetBorrowRequestResponse>> GetAllAsync();
    GetBorrowRequestResponse? GetById(int id);
    Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel);
    Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel);
}