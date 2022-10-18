using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentManagementContext _context;

    public StudentRepository(StudentManagementContext context)
    {
        _context = context;
    }

    public Student? Create(Student createStudent)
    {
        _context.Students.Add(createStudent);

        bool isSucceeded = _context.SaveChanges() > 0;

        return isSucceeded ? createStudent : null;
    }

    public bool Delete(int id)
    {
        var deleteStudent = _context.Students.FirstOrDefault(student => student.Id == id);

        if (deleteStudent == null) return false;

        _context.Students.Remove(deleteStudent);

        bool isSucceeded = _context.SaveChanges() > 0;

        return isSucceeded;
    }

    public IEnumerable<Student> GetAll()
    {
        return _context.Students;
    }

    public Student? GetById(int id)
    {
        return _context.Students.FirstOrDefault(student => student.Id == id);
    }

    public Student? Update(Student updateStudent)
    {
        _context.Students.Update(updateStudent);

        bool isSucceeded = _context.SaveChanges() > 0;

        return isSucceeded ? updateStudent : null;
    }
}