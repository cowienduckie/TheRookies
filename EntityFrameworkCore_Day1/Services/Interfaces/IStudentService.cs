using StudentManagement.Dtos;

namespace StudentManagement.Services;

public interface IStudentService
{
    IEnumerable<GetStudentResponse> GetAll();
    GetStudentResponse? GetById(int id);
    AddStudentResponse? Create(AddStudentRequest createModel);
    UpdateStudentResponse? Update(int id, UpdateStudentRequest updateModel);
    bool Delete(int id);
}