using BookLibrary.Data;
using BookLibrary.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddDbContext<BookLibraryContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ConfigureRepositories();

builder.Services.ConfigureServices();

builder.Services.ConfigureUnitOfWork();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
