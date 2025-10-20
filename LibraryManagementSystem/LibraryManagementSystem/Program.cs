using LibraryManagementSystem.Controllers;
using LibraryManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();

var app = builder.Build();

app.UseRouting();

app.MapControllers();
app.Run();