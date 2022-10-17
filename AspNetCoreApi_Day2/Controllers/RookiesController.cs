using Microsoft.AspNetCore.Mvc;
using AspNetCoreAPi.Services;
using AspNetCoreAPi.Models;

namespace AspNetCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RookiesController : ControllerBase
{
    private readonly IPersonService _personService;

    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PersonModel>> GetAll(string? name, string? gender, string? birthPlace)
    {
        try
        {
            var queryEntities = _personService.GetAll(name, gender, birthPlace);

            return Ok(queryEntities);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    public ActionResult<PersonModel> Create([FromBody] PersonCreateModel createModel)
    {
        if (createModel == null) return BadRequest();

        try
        {
            var createdEntity = _personService.Create(createModel);

            return createdEntity != null
                ? CreatedAtRoute(new { id = createdEntity.Id.ToString() }, createdEntity)
                : StatusCode(500);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<PersonModel> Update(Guid id, [FromBody] PersonUpdateModel updateModel)
    {
        var isPersonExist = _personService.IsExist(id);

        if (!isPersonExist) return NotFound();

        if (updateModel == null) return BadRequest();

        try
        {
            var updatedEntity = _personService.Update(id, updateModel);

            return updatedEntity != null ? Ok(updatedEntity) : StatusCode(500);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var isPersonExist = _personService.IsExist(id);

        if (!isPersonExist) return NotFound();

        try
        {
            var isSucceeded = _personService.Delete(id);

            return isSucceeded ? NoContent() : StatusCode(500);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    private ActionResult HandleException(Exception exception)
    {
        Console.WriteLine(exception);

        return StatusCode(500, exception);
    }
}