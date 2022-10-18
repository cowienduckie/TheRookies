using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data;

public class StudentManagementContext : DbContext
{
    public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
    : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
}