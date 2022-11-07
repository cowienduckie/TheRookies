using System.Linq.Expressions;
using System.Reflection;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.DataType;
using Common.Enums;

namespace BookLibrary.WebApi.Services.Implements;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = bookRepository;
        _categoryRepository = _unitOfWork.GetRepository<Category>();
    }

    public async Task<CreateBookResponse?> CreateAsync(CreateBookRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var categoryIds = requestModel.CategoryIds.Distinct();

            var categories = await _categoryRepository
                    .GetAllAsync(category => categoryIds.Contains(category.Id))
                as List<Category>;

            if (categories == null ||
                categories.Count != categoryIds.Count())
                return null;

            var newBook = new Book
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                Cover = requestModel.Cover ?? Settings.BaseBookCoverUrl,
                Categories = categories
            };

            var createdBook = _bookRepository.Create(newBook);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new CreateBookResponse
            {
                Id = createdBook.Id,
                Name = createdBook.Name,
                Description = createdBook.Description,
                Cover = createdBook.Cover,
                Categories = createdBook.Categories
                    .Select(category => new CategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name
                    })
                    .ToList()
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var book = await _bookRepository.GetAsync(book => book.Id == id);

            if (book == null) return false;

            _bookRepository.Delete(book);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return false;
        }
    }

    public async Task<PagedList<GetBookResponse>> GetAllAsync(
        PagingFilter pagingFilter, 
        SortFilter sortFilter)
    {
        var books = (await _bookRepository.GetAllAsync()).AsQueryable();

        var validSortFields = new []
        {
            SortField.Id,
            SortField.Name
        };

        var sortedBooks = books
            .SortData(validSortFields, sortFilter.Field, sortFilter.Order)
            .Select(book => new GetBookResponse(book))
            .AsQueryable();

        return new PagedList<GetBookResponse>
            (sortedBooks, pagingFilter.PageIndex, pagingFilter.PageSize);
    }

    public async Task<GetBookResponse?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetAsync(book => book.Id == id);

        return book == null ? null : new GetBookResponse(book);
    }

    public bool IsExist(int id)
    {
        return _bookRepository.IsExist(book => book.Id == id);
    }

    public async Task<UpdateBookResponse?> UpdateAsync(UpdateBookRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var categoryIds = requestModel.CategoryIds.Distinct();

            var categories = await _categoryRepository
                    .GetAllAsync(category => categoryIds.Contains(category.Id))
                as List<Category>;

            if (categories == null ||
                categories.Count != categoryIds.Count())
                return null;

            var book = await _bookRepository.GetAsync(book => book.Id == requestModel.Id);

            if (book == null) return null;

            book.Name = requestModel.Name;
            book.Description = requestModel.Description;
            book.Cover = requestModel.Cover ?? book.Cover;
            book.Categories = categories;

            var updatedBook = _bookRepository.Update(book);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new UpdateBookResponse
            {
                Id = updatedBook.Id,
                Name = updatedBook.Name,
                Description = updatedBook.Description,
                Cover = updatedBook.Cover,
                Categories = updatedBook.Categories
                    .Select(category => new CategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name
                    })
                    .ToList()
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }
}