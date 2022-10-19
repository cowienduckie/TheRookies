using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(StudentManagementContext context) : base(context)
    {
    }
}