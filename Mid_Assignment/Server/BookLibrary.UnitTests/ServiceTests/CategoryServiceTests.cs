using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Services.Implements;
using Common.DataType;
using Common.Enums;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class CategoryServiceTests
{
    private const int _categoryId = 1;
    private const string _categoryName = "Category";
    private Mock<ICategoryRepository> _categoryRepository;
    private CategoryService _categoryService;
    private Mock<IUnitOfWork> _unitOfWork;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = new Mock<ICategoryRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _categoryService = new CategoryService(_unitOfWork.Object, _categoryRepository.Object);
    }

    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsGetCategoryResponse()
    {
        var expected = new GetCategoryResponse
        {
            Id = _categoryId,
            Name = _categoryName
        };

        var categoryEntity = new Category
        {
            Id = _categoryId,
            Name = _categoryName
        };

        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntity);

        var result = await _categoryService.GetByIdAsync(It.IsAny<int>());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<GetCategoryResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));
        });
    }

    [Test]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(null as Category);

        var result = await _categoryService.GetByIdAsync(It.IsAny<int>());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllAsync_ReturnsListOfGetCategoryResponse()
    {
        var categories = new List<Category>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName}
        };

        var expected = new List<GetCategoryResponse>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName}
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categories);

        var result = (await _categoryService.GetAllAsync()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.AssignableTo<IEnumerable<GetCategoryResponse>>());

            Assert.That(result, Has.Count.EqualTo(expected.Count));
        });
    }

    [Test]
    public async Task CreateAsync_ReturnsCreateCategoryResponse()
    {
        var input = new CreateCategoryRequest
        {
            Name = _categoryName
        };

        var categoryEntity = new Category
        {
            Id = _categoryId,
            Name = _categoryName
        };

        var expected = new CreateCategoryResponse
        {
            Id = _categoryId,
            Name = _categoryName
        };

        _categoryRepository
            .Setup(cr => cr.Create(It.IsAny<Category>()))
            .Returns(categoryEntity);

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _categoryService.CreateAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<CreateCategoryResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));
        });
    }

    [Test]
    public async Task UpdateAsync_ValidId_ReturnsUpdateCategoryResponse()
    {
        var input = new UpdateCategoryRequest
        {
            Id = _categoryId,
            Name = _categoryName
        };

        var categoryEntity = new Category
        {
            Id = _categoryId,
            Name = _categoryName
        };

        var expected = new UpdateCategoryResponse
        {
            Id = _categoryId,
            Name = _categoryName
        };

        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntity);

        _categoryRepository
            .Setup(cr => cr.Update(It.IsAny<Category>()))
            .Returns(categoryEntity);

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _categoryService.UpdateAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<UpdateCategoryResponse>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));
        });
    }

    [Test]
    public async Task UpdateAsync_InvalidId_ReturnsNull()
    {
        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(null as Category);

        var result = await _categoryService.UpdateAsync(It.IsAny<UpdateCategoryRequest>());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_ValidId_ReturnsTrue()
    {
        var categoryEntity = new Category
        {
            Id = _categoryId,
            Name = _categoryName
        };

        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categoryEntity);

        _categoryRepository
            .Setup(cr => cr.Delete(It.IsAny<Category>()))
            .Verifiable();

        _unitOfWork
            .Setup(uow => uow.SaveChangesAsync())
            .ReturnsAsync(It.IsAny<int>());

        var result = await _categoryService.DeleteAsync(It.IsAny<int>());

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(null as Category);

        var result = await _categoryService.DeleteAsync(It.IsAny<int>());

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetPagedListAsync_ReturnsPagedListOfGetCategoryResponse()
    {
        var categories = new List<Category>
        {
            new() {Id = 1, Name = _categoryName},
            new() {Id = 2, Name = _categoryName},
            new() {Id = 3, Name = _categoryName}
        };

        _categoryRepository
            .Setup(cr => cr.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .ReturnsAsync(categories);

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

        var result = await _categoryService.GetPagedListAsync(pagingFilter, sortFilter);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.AssignableTo<IPagedList<GetCategoryResponse>>());

            Assert.That(result, Has.Count.EqualTo(1));

            Assert.That(result.PageIndex, Is.EqualTo(2));

            Assert.That(result.PageSize, Is.EqualTo(2));

            Assert.That(result.TotalPage, Is.EqualTo(2));

            Assert.That(result.TotalRecord, Is.EqualTo(3));

            Assert.That(result.HasNextPage, Is.False);

            Assert.That(result.HasPreviousPage, Is.True);

            Assert.That(result[0], Is.InstanceOf<GetCategoryResponse>());

            Assert.That(result[0].Id, Is.EqualTo(1));

            Assert.That(result[0].Name, Is.EqualTo(_categoryName));
        });
    }
}