using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBorrowRequestService
{
    IEnumerable<GetBorrowRequestResponse> GetAll();
    GetBorrowRequestResponse? GetById(int id);
    CreateBorrowRequestResponse? Create(CreateBorrowRequestRequest requestModel);
    ApproveBorrowRequestResponse? Update(ApproveBorrowRequestRequest requestModel);
}