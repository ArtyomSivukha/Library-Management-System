using LibraryManagementSystem.Services;
using LibraryManagementSystem.Services.Local;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapControllers();
app.Run();