using AspNetCoreFundamental.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseLoggingRequest();

app.Run();
