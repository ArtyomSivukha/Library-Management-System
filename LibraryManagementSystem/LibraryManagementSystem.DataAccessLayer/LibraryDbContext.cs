using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding(DbSeedData.SeedData);
    }
}