using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Interfaces;

namespace BookLibrary.WebApi.Services.Implements;

public class BookService : IBookService
{
    public IEnumerable<GetCategoryResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public GetBookResponse? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public CreateBookResponse? Create(CreateBookRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public UpdateBookResponse? Update(UpdateBookRequest requestModel)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}