using AspNetCoreApi.Models;

namespace AspNetCoreApi.Services;

public interface ITaskService
{
    IEnumerable<TaskModel> GetAll();
    TaskModel? GetById(Guid id);
    TaskModel? Create(TaskCreateModel createModel);
    TaskModel? Update(Guid id, TaskUpdateModel updateModel);
    bool Delete(Guid id);
    Task BulkCreate(IEnumerable<TaskCreateModel> createModels);
    Task BulkDelete(IEnumerable<Guid> deleteIds);
    bool IsExist(Guid id);
}