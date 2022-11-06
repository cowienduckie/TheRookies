using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class BorrowRequestService : IBorrowRequestService
{
    public Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetBorrowRequestResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public GetBorrowRequestResponse? GetById(int id)
    {
        throw new NotImplementedException();
    }
}