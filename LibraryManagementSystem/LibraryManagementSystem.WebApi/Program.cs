// using LibraryManagementSystem.Services;
// using LibraryManagementSystem.Services.EntityFramework.Entities;
// using LibraryManagementSystem.Services.Local;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddControllers();
// builder.Services.AddScoped<IAuthorService,AuthorService>();
// builder.Services.AddScoped<IBookService, BookService>();
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseRouting();
//
// app.MapControllers();
// app.Run();


using LibraryManagementSystem.Services.EntityFramework.Entities;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

using (var context = new LibraryDbContext(configuration)) {

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    
    var aut1 = new Author() { Name = "name" };
    var bok1 = new Book() {  Title = "title"};
    
    context.Books.Add(bok1);
    
    context.SaveChanges();
    
    foreach (var s in context.Books) {
        Console.WriteLine($"Title: {s.Title}");
    }
}