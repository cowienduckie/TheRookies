using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.Category;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBookService
{
    IEnumerable<GetCategoryResponse> GetAll();
    GetBookResponse? GetById(int id);
    CreateBookResponse? Create(CreateBookRequest requestModel);
    UpdateBookResponse? Update(UpdateBookRequest requestModel);
    bool Delete(int id);
}