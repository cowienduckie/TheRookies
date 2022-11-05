using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class BorrowRequestService : IBorrowRequestService
{
    public IEnumerable<GetBorrowRequestResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public GetBorrowRequestResponse? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public CreateBorrowRequestResponse? Create(CreateBorrowRequestRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public ApproveBorrowRequestResponse? Update(ApproveBorrowRequestRequest requestModel)
    {
        throw new NotImplementedException();
    }
}