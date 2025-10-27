using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Services.EntityFramework.Entities;

public class LibraryDbContext : DbContext
{
    private readonly IConfiguration _appConfig;

    public LibraryDbContext(IConfiguration appConfig)
    {
        _appConfig = appConfig;
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_appConfig.GetConnectionString("LibraryDbContext"));
    }
    

}