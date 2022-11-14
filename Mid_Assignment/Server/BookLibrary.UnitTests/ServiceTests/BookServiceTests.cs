using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Services.Implements;
using Common.DataType;
using Common.Enums;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class BookServiceTests
{
    private const int _bookId = 1;
    private const string _bookName = "Book";
    private const string _categoryName = "Category";
    private Mock<IBookRepository> _bookRepository;
    private BookService _bookService;
    private Mock<ICategoryRepository> _categoryRepository;
    private Mock<IUnitOfWork> _unitOfWork;

    [SetUp]
    public void Setup()
    {
        _bookRepository = new Mock<IBookRepository>();
        _categoryRepository = new Mock<ICategoryRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _bookService = new BookService(
            _bookRepository.Object,
            _categoryRepository.Object,
            _unitOfWork.Object);
    }

    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsGetBookResponse()
    {
        var bookEntity = new Book
        {
            Id = _bookId,
            Name = _bookName,
            Categories = new List<Category>
            {
                new() {Id = 1, Name = _categoryName},
                new() {Id = 2, Name = _categoryName}
            }
        };

        var expected = new GetBookResponse(bookEntity);

        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(bookEntity);

        var result = await _bookService.GetByIdAsync(It.IsAny<int>());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<GetBookResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));

            Assert.That(result?.Description, Is.EqualTo(expected.Description));

            Assert.That(result?.Cover, Is.EqualTo(expected.Cover));

            Assert.That(result?.Categories, Is.InstanceOf<List<CategoryModel>>());

            Assert.That(result?.Categories, Has.Count.EqualTo(expected.Categories.Count));
        });
    }

    [Test]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(null as Book);

        var result = await _bookService.GetByIdAsync(It.IsAny<int>());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsync_ValidCategoryIds_ReturnsCreateBookResponse()
    {
        var input = new CreateBookRequest
        {
            Name = _bookName,
            CategoryIds = new List<int> {1, 2}
        };

        var categoryEntities = new List<Category>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName}
        };

        var bookEntity = new Book
        {
            Id = _bookId,
            Name = _bookName,
            Categories = categoryEntities
        };

        var expected = new CreateBookResponse
        {
            Id = _bookId,
            Name = _bookName,
            Categories = new List<CategoryModel>
            {
                new() {Id = 1, Name = _categoryName},
                new() {Id = 2, Name = _categoryName}
            }
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntities);

        _bookRepository
            .Setup(cr => cr.Create(It.IsAny<Book>()))
            .Returns(bookEntity);

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _bookService.CreateAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<CreateBookResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));

            Assert.That(result?.Categories, Is.InstanceOf<List<CategoryModel>>());

            Assert.That(result?.Categories, Has.Count.EqualTo(expected.Categories.Count));
        });
    }

    [Test]
    public async Task CreateAsync_InvalidCategoryIds_ReturnsNull()
    {
        var input = new CreateBookRequest
        {
            Name = _bookName,
            CategoryIds = new List<int> {1, 2}
        };

        var categoryEntities = new List<Category>
        {
            new() {Id = 1, Name = _categoryName}
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntities);

        var result = await _bookService.CreateAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task UpdateAsync_ValidInput_ReturnsUpdateBookResponse()
    {
        var input = new UpdateBookRequest
        {
            Id = _bookId,
            Name = _bookName,
            CategoryIds = new List<int> {1, 2}
        };

        var categoryEntities = new List<Category>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName}
        };

        var bookEntity = new Book
        {
            Id = _bookId,
            Name = _bookName,
            Categories = categoryEntities
        };

        var expected = new UpdateBookResponse
        {
            Id = _bookId,
            Name = _bookName,
            Categories = new List<CategoryModel>
            {
                new() {Id = 1, Name = _categoryName},
                new() {Id = 2, Name = _categoryName}
            }
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntities);

        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(bookEntity);

        _bookRepository
            .Setup(cr => cr.Update(It.IsAny<Book>()))
            .Returns(bookEntity);

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _bookService.UpdateAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<UpdateBookResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));

            Assert.That(result?.Categories, Is.InstanceOf<List<CategoryModel>>());

            Assert.That(result?.Categories, Has.Count.EqualTo(expected.Categories.Count));
        });
    }

    [Test]
    public async Task UpdateAsync_InvalidId_ReturnsNull()
    {
        var input = new UpdateBookRequest
        {
            Id = _bookId,
            Name = _bookName,
            CategoryIds = new List<int> {1, 2}
        };

        var categoryEntities = new List<Category>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName}
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntities);

        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(null as Book);

        var result = await _bookService.UpdateAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task UpdateAsync_InvalidCategoryIds_ReturnsNull()
    {
        var input = new UpdateBookRequest
        {
            Id = _bookId,
            Name = _bookName,
            CategoryIds = new List<int> {1, 2}
        };

        var categoryEntities = new List<Category>
        {
            new() {Id = 1, Name = _categoryName}
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntities);

        var result = await _bookService.UpdateAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(new Book());

        _bookRepository
            .Setup(cr => cr.Delete(It.IsAny<Book>()))
            .Verifiable();

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _bookService.DeleteAsync(It.IsAny<int>());

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        _bookRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(null as Book);

        var result = await _bookService.DeleteAsync(It.IsAny<int>());

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetPagedListAsync_ReturnsPagedListOfGetBookResponse()
    {
        var books = new List<Book>
        {
            new()
            {
                Id = 1,
                Name = _bookName,
                Categories = new List<Category>
                {
                    new() {Id = 1, Name = _categoryName},
                    new() {Id = 2, Name = _categoryName}
                }
            },
            new()
            {
                Id = 2,
                Name = _bookName,
                Categories = new List<Category>
                {
                    new() {Id = 1, Name = _categoryName},
                    new() {Id = 2, Name = _categoryName}
                }
            },
            new()
            {
                Id = 3,
                Name = _bookName,
                Categories = new List<Category>
                {
                    new() {Id = 1, Name = _categoryName},
                    new() {Id = 2, Name = _categoryName}
                }
            }
        };

        _bookRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(books);

        var pagingFilter = new PagingFilter
        {
            PageIndex = 2,
            PageSize = 2
        };

        var sortFilter = new SortFilter
        {
            SortField = SortField.Id,
            SortOrder = SortOrder.Descending
        };

        var result = await _bookService.GetPagedListAsync(pagingFilter, sortFilter);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.AssignableTo<IPagedList<GetBookResponse>>());

            Assert.That(result, Has.Count.EqualTo(1));

            Assert.That(result.PageIndex, Is.EqualTo(2));

            Assert.That(result.PageSize, Is.EqualTo(2));

            Assert.That(result.TotalPage, Is.EqualTo(2));

            Assert.That(result.TotalRecord, Is.EqualTo(3));

            Assert.That(result.HasNextPage, Is.False);

            Assert.That(result.HasPreviousPage, Is.True);

            Assert.That(result[0], Is.InstanceOf<GetBookResponse>());

            Assert.That(result[0].Id, Is.EqualTo(1));

            Assert.That(result[0].Name, Is.EqualTo(_bookName));

            Assert.That(result[0].Categories, Is.InstanceOf<List<CategoryModel>>());

            Assert.That(result[0].Categories, Has.Count.EqualTo(2));
        });
    }
}