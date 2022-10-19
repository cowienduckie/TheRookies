using AspNetCoreApi.Models;

namespace AspNetCoreApi.Services;

public class TaskService : ITaskService
{
    private static readonly List<TaskModel> _taskList = new()
    {
        new TaskModel("First task"),
        new TaskModel("Second task")
    };

    public Task BulkCreate(IEnumerable<TaskCreateModel> createModels)
    {
        var createEntities = createModels.Select(task => new TaskModel(task.Title));

        _taskList.AddRange(createEntities);

        return Task.CompletedTask;
    }

    public Task BulkDelete(IEnumerable<Guid> deleteIds)
    {
        bool isSucceeded = true;
        var deletedEntities = new List<TaskModel>();

        foreach (var id in deleteIds)
        {
            var entity = _taskList.FirstOrDefault(task => task.Id == id);

            if (entity == null)
            {
                isSucceeded = false;

                break;
            }

            deletedEntities.Add(entity);

            _taskList.Remove(entity);
        }

        if (!isSucceeded)
        {
            _taskList.AddRange(deletedEntities);
        }

        return Task.CompletedTask;
    }

    public TaskModel? Create(TaskCreateModel createModel)
    {
        var createEntity = new TaskModel(createModel.Title);

        _taskList.Add(createEntity);

        return createEntity;
    }

    public bool Delete(Guid id)
    {
        var entity = _taskList.First(task => task.Id == id);

        return _taskList.Remove(entity);
    }

    public IEnumerable<TaskModel> GetAll()
    {
        return _taskList;
    }

    public TaskModel? GetById(Guid id)
    {
        return _taskList.FirstOrDefault(task => task.Id == id);
    }

    public bool IsExist(Guid id)
    {
        return _taskList.Any(task => task.Id == id);
    }

    public TaskModel? Update(Guid id, TaskUpdateModel updateModel)
    {
        var entity = _taskList.First(task => task.Id == id);

        entity.Title = updateModel.Title;
        entity.IsCompleted = updateModel.IsCompleted;

        return entity;
    }
}