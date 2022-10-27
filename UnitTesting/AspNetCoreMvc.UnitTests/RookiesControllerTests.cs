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
    public void Index_ReturnsViewWithListOfPersonViewModel()
    {
        var expectedList = new List<PersonViewModel>
        {
            new PersonViewModel(),
            new PersonViewModel()
        };

        _personService
            .Setup(ps => ps.GetAllPeople())
            .Returns(expectedList);

        var result = _rookiesController.Index();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = ((ViewResult)result).Model;

            Assert.That(model, Is.AssignableTo<IEnumerable<PersonViewModel>>());

            Assert.That(model as List<PersonViewModel>, Has.Count.EqualTo(expectedList.Count));
        });
    }

    [Test]
    public void AddHttpGet_ReturnsViewWithPersonCreateModel()
    {
        var result = _rookiesController.Add();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = ((ViewResult)result).Model;

            Assert.That(model, Is.Not.Null);

            Assert.That(model, Is.InstanceOf<PersonCreateModel>());
        });
    }

    [Test]
    public void AddHttpPost_ReturnsRedirectToIndexAction()
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
    public void Details_InvalidIndex_ReturnsRedirectToIndexAction()
    {
        _personService
            .Setup(p => p.GetPersonByIndex(It.IsAny<int>()))
            .Returns(null as PersonViewModel);

        const int index = -1;

        var result = _rookiesController.Details(index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Index"));
        });
    }

    [Test]
    public void Details_ValidIndex_ReturnsViewWithPersonViewModel()
    {
        var expectedModel = new PersonViewModel
        {
            FirstName = "Minh",
            LastName = "Tran"
        };

        _personService.Setup(ps => ps.GetPersonByIndex(It.IsAny<int>())).Returns(expectedModel);

        const int index = 0;

        var result = _rookiesController.Details(index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = ((ViewResult)result).Model;

            Assert.That(model, Is.Not.Null);

            Assert.That(model, Is.InstanceOf<PersonViewModel>());

            Assert.That(((PersonViewModel?)model)?.FirstName, Is.EqualTo(expectedModel.FirstName));

            Assert.That(((PersonViewModel?)model)?.LastName, Is.EqualTo(expectedModel.LastName));
        });
    }

    [Test]
    public void EditHttpGet_InvalidIndex_ReturnsRedirectToIndexAction()
    {
        _personService
            .Setup(p => p.GetPersonEditModel(It.IsAny<int>()))
            .Returns(null as PersonEditModel);

        const int index = -1;

        var result = _rookiesController.Edit(index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            Assert.That(((RedirectToActionResult)result).ActionName, Is.EqualTo("Index"));
        });
    }

    [Test]
    public void EditHttpGet_ValidIndex_ReturnsViewWithPersonEditModel()
    {
        var expectedModel = new PersonEditModel
        {
            FirstName = "Minh",
            LastName = "Tran"
        };

        _personService.Setup(ps => ps.GetPersonEditModel(It.IsAny<int>())).Returns(expectedModel);

        const int index = 0;

        var result = _rookiesController.Edit(index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = ((ViewResult)result).Model;

            Assert.That(model, Is.Not.Null);

            Assert.That(model, Is.InstanceOf<PersonEditModel>());

            Assert.That(((PersonEditModel?)model)?.FirstName, Is.EqualTo(expectedModel.FirstName));

            Assert.That(((PersonEditModel?)model)?.LastName, Is.EqualTo(expectedModel.LastName));
        });
    }
}