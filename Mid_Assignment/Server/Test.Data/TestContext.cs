using Microsoft.EntityFrameworkCore;
using Test.Data.Entities;

namespace Test.Data;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
}