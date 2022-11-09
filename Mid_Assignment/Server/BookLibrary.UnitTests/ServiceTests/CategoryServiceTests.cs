using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.Data.Repositories;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Services.Implements;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class CategoryServiceTests
{
    private CategoryService _categoryService;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<BaseRepository<Category>> _categoryRepository;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = new Mock<BaseRepository<Category>>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _unitOfWork
            .Setup(uow => uow.GetRepository<Category>())
            .Returns(_categoryRepository.Object);

        _categoryService = new CategoryService(_unitOfWork.Object);
    }

    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsGetCategoryResponse()
    {
        const int inputId = 1;
        const string inputName = "Category";

        var expected = new GetCategoryResponse
        {
            Id = inputId,
            Name = inputName
        };

        _categoryRepository
            .Setup(cr => cr.GetSingleAsync(c => c.Id == It.IsAny<int>()))
            .ReturnsAsync(new Category
            {
                Id = inputId,
                Name = inputName
            });

        var result = await _categoryService.GetByIdAsync(inputId);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<GetCategoryResponse>());

            Assert.That(result, Is.Not.Null);

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Name, Is.EqualTo(expected.Name));
        });
    }
}