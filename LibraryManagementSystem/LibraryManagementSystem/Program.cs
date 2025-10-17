using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<BookService>();


var app = builder.Build();

app.UseRouting();

app.MapControllers();
app.Run();