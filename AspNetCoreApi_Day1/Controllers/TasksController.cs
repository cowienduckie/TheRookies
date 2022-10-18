using Microsoft.AspNetCore.Mvc;
using AspNetCoreApi.Models;
using AspNetCoreApi.Services;

namespace AspNetCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TaskModel>> GetAll()
    {
        try
        {
            var entities = _taskService.GetAll();

            return Ok(entities);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<TaskModel> GetById(Guid id)
    {
        try
        {
            var entity = _taskService.GetById(id);

            return entity != null ? Ok(entity) : NotFound();
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost]
    public ActionResult<TaskModel> Create([FromBody] TaskCreateModel createModel)
    {
        if (createModel == null) return BadRequest();

        try
        {
            var createdEntity = _taskService.Create(createModel);

            return createdEntity != null
                ? CreatedAtRoute(new { id = createdEntity.Id.ToString() }, createdEntity)
                : StatusCode(500);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost("bulk")]
    public ActionResult<IEnumerable<TaskModel>> BulkCreate([FromBody] List<TaskCreateModel> createModels)
    {
        if (createModels == null) return BadRequest();

        try
        {
            var createdEntities = _taskService.BulkCreate(createModels);

            return Ok(createdEntities);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<TaskModel> Update(Guid id, [FromBody] TaskUpdateModel updateModel)
    {
        var isTaskExist = _taskService.IsExist(id);

        if (!isTaskExist) return NotFound();

        if (updateModel == null) return BadRequest();

        try
        {
            var updatedEntity = _taskService.Update(id, updateModel);

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
        var isTaskExist = _taskService.IsExist(id);

        if (!isTaskExist) return NotFound();

        try
        {
            var isSucceeded = _taskService.Delete(id);

            return isSucceeded ? NoContent() : StatusCode(500);
        }
        catch (Exception exception)
        {
            return HandleException(exception);
        }
    }

    [HttpPost("bulk-deletion")]
    public ActionResult BulkDelete(List<Guid> deleteIds)
    {
        try
        {
            var isSucceeded = _taskService.BulkDelete(deleteIds);

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

        return StatusCode(500);
    }
}