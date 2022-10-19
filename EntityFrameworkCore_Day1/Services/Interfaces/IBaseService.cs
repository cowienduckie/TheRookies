using StudentManagement.Models;

namespace StudentManagement.Services;

public interface IBaseService<T, K> where T : class where K : class
{
    // IEnumerable<StudentViewModel> GetAll();
    // StudentViewModel? GetById(int id);
    T Create(K createModel);
    // StudentViewModel? Update(int id, StudentUpdateModel updateModel);
    // bool Delete(int id);
}