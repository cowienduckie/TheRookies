using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Filters;
using Common.DataType;

namespace BookLibrary.WebApi.Services.Interfaces;

public interface IBookService
{
    Task<PagedList<GetBookResponse>> GetAllAsync(PagingFilter pagingFilter, SortFilter sortFilter);
    Task<GetBookResponse?> GetByIdAsync(int id);
    Task<CreateBookResponse?> CreateAsync(CreateBookRequest requestModel);
    Task<UpdateBookResponse?> UpdateAsync(UpdateBookRequest requestModel);
    Task<bool> Delete(int id);
    bool IsExist(int id);
}