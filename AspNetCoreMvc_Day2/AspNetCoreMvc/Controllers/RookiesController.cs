using AspNetCoreMvc.Services;
using AspNetCoreMvc.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers;

public class RookiesController : Controller
{
    private readonly PersonService _personService;

    public RookiesController()
    {
        _personService = new PersonService();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModels = _personService.GetAllPeople();

        return View(viewModels);
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
}