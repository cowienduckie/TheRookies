using BookLibrary.Data;
using BookLibrary.WebApi.Extensions;
using BookLibrary.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddDbContext<BookLibraryContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DockerConnection"))
);

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

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();