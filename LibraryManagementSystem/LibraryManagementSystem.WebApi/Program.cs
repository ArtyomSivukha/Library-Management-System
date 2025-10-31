using LibraryManagementSystem.Services;
using LibraryManagementSystem.Services.EntityFramework;
using LibraryManagementSystem.Services.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbContext"));
});

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseRouting();

app.MapControllers();
app.Run();
