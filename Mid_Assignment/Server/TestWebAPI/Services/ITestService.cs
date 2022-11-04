using Test.Data.Entities;

namespace TestWebAPI.Services;

public interface ITestService
{
    Task<IEnumerable<Person>> Get();

    Task Add();
}