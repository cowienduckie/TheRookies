using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;

namespace BookLibrary.WebApi.Services.Implements;

public class BookService : IBookService
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _bookRepository = _unitOfWork.GetRepository<Book>();
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

            if (categories != null &&
                categories.Count != categoryIds.Count())
            {
                await transaction.RollbackAsync();

                return null;
            }

            var newBook = new Book
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                Cover = requestModel.Cover ?? CommonConstants.BaseBookCoverUrl,
                Categories = categories.ToList()
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

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GetBookResponse>> GetAllAsync()
    {
        return (await _bookRepository.GetAllAsync())
            .Select(book => new GetBookResponse
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                Cover = book.Cover,
                Categories = book.Categories
                    .Select(category => new CategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name
                    })
                    .ToList()
            });
    }

    public async Task<GetBookResponse?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetAsync(book => book.Id == id);

        if (book == null) return null;

        return new GetBookResponse
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description,
            Cover = book.Cover,
            Categories = book.Categories
                .Select(category => new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name
                })
                .ToList()
        };
    }

    public bool IsExist(int id)
    {
        return _bookRepository.IsExist(book => book.Id == id);
    }

    public Task<UpdateBookResponse?> UpdateAsync(UpdateBookRequest requestModel)
    {
        throw new NotImplementedException();
    }
}