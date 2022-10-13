using Microsoft.AspNetCore.Mvc;
using AspNetCoreApi.Models;

namespace AspNetCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskModel> _taskList = new()
    {
        new TaskModel("First task", false),
        new TaskModel("Second task", true)
    };

    [HttpGet]
    public ActionResult<IEnumerable<TaskModel>> GetAll()
    {
        return Ok(_taskList);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskModel> GetById(Guid id)
    {
        var task = _taskList.Find(task => task.Id == id);

        return task != null ? Ok(task) : NotFound();
    }

    [HttpPost]
    public ActionResult<TaskModel> Create([FromBody] TaskCreateModel createModel)
    {
        if (createModel == null) return BadRequest();

        var newTask = new TaskModel(createModel.Title, false);

        _taskList.Add(newTask);

        return CreatedAtRoute(new { id = newTask.Id.ToString() }, newTask);
    }

    [HttpPost("bulk-create")]
    public ActionResult<IEnumerable<TaskModel>> CreateRange([FromBody] List<TaskCreateModel> createModels)
    {
        if (createModels == null) return BadRequest();

        var newTasks = createModels
            .ConvertAll(task => new TaskModel(task.Title, false));

        _taskList.AddRange(newTasks);

        return Ok(newTasks);
    }

    [HttpPut("{id}")]
    public ActionResult<TaskModel> Update(Guid id, [FromBody] TaskUpdateModel updateModel)
    {
        if (updateModel == null) return BadRequest();

        var task = _taskList.Find(task => task.Id == id);

        if (task == null) return NotFound();

        task.Title = updateModel.Title;
        task.IsCompleted = updateModel.IsCompleted;

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var task = _taskList.Find(task => task.Id == id);

        if (task == null) return NotFound();

        _taskList.Remove(task);

        return Ok();
    }

    [HttpDelete("bulk-delete")]
    public ActionResult DeleteRange(List<Guid> deleteIds)
    {
        bool isAllValid = true;
        var deleteTasks = new List<TaskModel>();

        foreach (var id  in deleteIds)
        {
            var deleteTask = _taskList.Find(task => task.Id == id);

            if (deleteTask == null)
            {
                isAllValid = false;

                break;
            }

            deleteTasks.Add(deleteTask);
        }

        if (!isAllValid) return BadRequest();

        foreach (var deleteTask in deleteTasks)
        {
            _taskList.Remove(deleteTask);
        }

        return Ok();
    }
}