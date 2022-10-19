using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Repositories;
using StudentManagement.Services;
using StudentManagement.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStudentService, StudentService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IStudentRepository, StudentRepository>();

builder.Services.AddDbContext<StudentManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
