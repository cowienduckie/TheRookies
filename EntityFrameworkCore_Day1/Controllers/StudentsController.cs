using Microsoft.AspNetCore.Mvc;
using StudentManagement.Dtos;
using StudentManagement.Services;

namespace StudentManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IEnumerable<GetStudentResponse> GetAll()
    {
        return _studentService.GetAll();
    }

    [HttpGet("{id}")]
    public GetStudentResponse? GetById(int id)
    {
        return _studentService.GetById(id);
    }

    [HttpPost]
    public AddStudentResponse? Add([FromBody] AddStudentRequest requestModel)
    {
        return _studentService.Create(requestModel);
    }

    [HttpPut("{id}")]
    public UpdateStudentResponse? Update(int id, [FromBody] UpdateStudentRequest requestModel)
    {
        return _studentService.Update(id, requestModel);
    }

    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _studentService.Delete(id);
    }
}