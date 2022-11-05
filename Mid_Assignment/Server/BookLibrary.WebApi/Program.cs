using BookLibrary.Data;
using BookLibrary.WebApi.Services.Implements;
using BookLibrary.WebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;
builder.Services.AddDbContext<BookLibraryContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
