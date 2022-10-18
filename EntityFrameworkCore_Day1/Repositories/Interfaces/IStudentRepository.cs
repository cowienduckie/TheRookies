using StudentManagement.Models;

namespace StudentManagement.Repositories;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    Student? Create(Student createStudent);
    Student? Update(Student updateStudent);
    bool Delete(int id);
}