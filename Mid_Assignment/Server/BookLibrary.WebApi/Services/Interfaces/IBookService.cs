using BookLibrary.WebApi.Dtos.Book;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<GetBookResponse>> GetAllAsync();
    Task<GetBookResponse?> GetByIdAsync(int id);
    Task<CreateBookResponse?> CreateAsync(CreateBookRequest requestModel);
    Task<UpdateBookResponse?> UpdateAsync(UpdateBookRequest requestModel);
    bool Delete(int id);
    bool IsExist(int id);
}