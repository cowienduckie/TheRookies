using StudentManagement.Models;

namespace StudentManagement.Services;

public interface IStudentService
{
    IEnumerable<StudentViewModel> GetAll();
    StudentViewModel? GetById(int id);
    int? Create(StudentCreateModel createModel);
    StudentViewModel? Update(int id, StudentUpdateModel updateModel);
    bool Delete(int id);
}