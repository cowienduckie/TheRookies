using AspNetCoreMvc.Services.Interfaces;
using AspNetCoreMvc.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers;

public class RookiesController : Controller
{
    private const string _deletedPersonSessionKey = "DeletedPersonName";
    private readonly IPersonService _personService;

    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModels = _personService.GetAllPeople();

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Details(int index)
    {
        var viewModel = _personService.GetPersonByIndex(index);

        ViewBag.Index = index;

        return viewModel != null ? View(viewModel) : RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Add()
    {
        var createModel = new PersonCreateModel();

        return View(createModel);
    }

    [HttpPost]
    public IActionResult Add(PersonCreateModel createModel)
    {
        if (ModelState.IsValid)
        {
            _personService.AddPerson(createModel);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int index)
    {
        var editModel = _personService.GetPersonEditModel(index);

        return editModel != null ? View(editModel) : RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(int index, PersonEditModel editModel)
    {
        if (ModelState.IsValid)
        {
            _personService.EditPerson(index, editModel);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int index)
    {
        _personService.DeletePerson(index);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteAndRedirectToResultView(int index)
    {
        var deletedPerson = _personService.DeletePerson(index);

        if (deletedPerson != null)
        {
            HttpContext.Session.SetString(_deletedPersonSessionKey, deletedPerson.FullName ?? string.Empty);

            return RedirectToAction("DeleteResult");
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteResult()
    {
        return View();
    }
}