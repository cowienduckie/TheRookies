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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateBookResponse?> CreateAsync(CreateBookRequest requestModel)
    {
        var categoryIds = requestModel.CategoryIds.Distinct();

        var categories = (List<Category>)await _categoryRepository
            .GetAllAsync(category => categoryIds.Contains(category.Id));

        if (categories.Count != categoryIds.Count())
        {
            return null;
        }

        var newBook = new Book
        {
            Name = requestModel.Name,
            Description = requestModel.Description,
            Cover = requestModel.Cover ?? Settings.BaseBookCoverUrl,
            Categories = categories
        };

        var createdBook = _bookRepository.Create(newBook);

        await _unitOfWork.SaveChangesAsync();

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

    public async Task<bool> Delete(int id)
    {
        var book = await _bookRepository.GetSingleAsync(book => book.Id == id);

        if (book == null) return false;

        _bookRepository.Delete(book);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<PagedList<GetBookResponse>> GetAllAsync(
        PagingFilter pagingFilter,
        SortFilter sortFilter)
    {
        var books = (await _bookRepository.GetAllAsync()).AsQueryable();

        var validSortFields = new[]
        {
            SortField.Id,
            SortField.Name
        };

        var sortedBooks = books
            .SortData(validSortFields, sortFilter.SortField, sortFilter.SortOrder)
            .Select(book => new GetBookResponse(book))
            .AsQueryable();

        return new PagedList<GetBookResponse>
            (sortedBooks, pagingFilter.PageIndex, pagingFilter.PageSize);
    }

    public async Task<GetBookResponse?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetSingleAsync(book => book.Id == id);

        return book == null ? null : new GetBookResponse(book);
    }

    public bool IsExist(int id)
    {
        return _bookRepository.IsExist(book => book.Id == id);
    }

    public async Task<UpdateBookResponse?> UpdateAsync(UpdateBookRequest requestModel)
    {
        var categoryIds = requestModel.CategoryIds.Distinct();

        var categories = (List<Category>)await _categoryRepository
            .GetAllAsync(category => categoryIds.Contains(category.Id));

        if (categories.Count != categoryIds.Count())
        {
            return null;
        }

        var book = await _bookRepository.GetSingleAsync(book => book.Id == requestModel.Id);

        if (book == null) return null;

        book.Name = requestModel.Name;
        book.Description = requestModel.Description;
        book.Cover = requestModel.Cover ?? book.Cover;
        book.Categories = categories;

        var updatedBook = _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync();

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
}