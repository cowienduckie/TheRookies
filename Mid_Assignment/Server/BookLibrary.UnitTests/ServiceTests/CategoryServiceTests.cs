using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Implements;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class CategoryServiceTests
{
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
    public async Task GetByIdAsync_ValidId_ReturnGetCategoryResponse()
    {
        const int categoryId = 1;
        const string categoryName = "Category";

        var expected = new GetCategoryResponse
        {
            Id = categoryId,
            Name = categoryName
        };

        var categoryEntity = new Category()
        {
            Id = categoryId,
            Name = categoryName
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
}