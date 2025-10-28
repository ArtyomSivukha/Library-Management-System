using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Services.EntityFramework.Entities;

public class LibraryDbContext : DbContext
{
    // private readonly IConfiguration _appConfig;
    //
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
}