using AspNetCoreMvc.Controllers;
using AspNetCoreMvc.Services.Interfaces;
using AspNetCoreMvc.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AspNetCoreMvc.UnitTests;

public class RookiesControllerTests
{
    private RookiesController _rookiesController;
    private Mock<IPersonService> _personService;

    [SetUp]
    public void Setup()
    {
        _personService = new Mock<IPersonService>();
        _rookiesController = new RookiesController(_personService.Object);
    }

    [Test]
    public void AddPerson_Success()
    {
        var mockModel = new PersonCreateModel
        {
            FirstName = "Minh",
            LastName = "Tran",
            Gender = "Male",
            DateOfBirth = new DateTime(2000, 04, 24)
        };

        var result = _rookiesController.Add(mockModel);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Index"));
        });
    }

    [Test]
    public void Details_ReturnsToAction_InvalidIndex()
    {
        _personService
            .Setup(p => p.GetPersonByIndex(It.IsAny<int>()))
            .Returns(null as PersonViewModel);

        const int index = 0;

        var result = _rookiesController.Details(index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Index"));
        });
    }

    [Test]
    public void Details_ReturnsView_ValidIndex()
    {
        var expectModel = new PersonViewModel
        {
            FirstName = "Minh",
            LastName = "Tran"
        };

        _personService.Setup(p => p.GetPersonByIndex(It.IsAny<int>())).Returns(expectModel);

        const int index = 0;

        var result = _rookiesController.Details(index) as ViewResult;

        Assert.That(result, Is.Not.Null);

        var returnModel = result?.Model as PersonViewModel;

        Assert.That(returnModel, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(returnModel?.FirstName, Is.EqualTo(expectModel.FirstName));

            Assert.That(returnModel?.LastName, Is.EqualTo(expectModel.LastName));
        });
    }
}