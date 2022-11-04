using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Data.Entities;

namespace TestWebAPI.Services;

public class TestService : ITestService
{
    private readonly TestContext _context;

    public TestService(TestContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> Get()
    {
        var people = await _context.People.ToListAsync();
        return people;
    }

    public async Task Add()
    {
        var p = new Person
        {
            Name = "David",
            Age = 22,
            Dob = DateTime.Now,
            Gender = GenderEnum.Male,
            EmailAddress = "test@gmail.com",
            LicenseId = "license to kill"
        };
        await _context.People.AddAsync(p);
        await _context.SaveChangesAsync();
    }
}