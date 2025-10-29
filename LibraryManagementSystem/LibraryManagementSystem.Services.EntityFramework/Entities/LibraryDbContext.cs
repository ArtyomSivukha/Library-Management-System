using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Services.EntityFramework.Entities;

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
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSeeding(SeedData)
                .UseAsyncSeeding(SeedDataAsync);

        }
    }

    private void SeedData(DbContext context, bool seed)
    {
        var hasAuthors = context.Set<Author>().Any();
        if (!hasAuthors)
        {
            var authors = new List<Author>
            {
                new() { Id = 1, Name = "First", DateOfBirth = new DateTime(1999, 9, 9) },
                new() { Id = 2, Name = "Second", DateOfBirth = new DateTime(1875, 11, 11) },
                new() { Id = 3, Name = "Third", DateOfBirth = new DateTime(1860, 1, 29) }
            };

            context.Set<Author>().AddRange(authors);
            context.SaveChanges();

            var books = new List<Book>
            {
                new() { Id = 1, Title = "Title first", PublisherYear = 1869, Author = authors[0] },
                new() { Id = 2, Title = "Title second", PublisherYear = 1877, Author = authors[1] },
                new() { Id = 3, Title = "Title third", PublisherYear = 1866, Author = authors[0] },
                new() { Id = 4, Title = "Title fourth", PublisherYear = 1869, Author = authors[0] },
                new() { Id = 5, Title = "Title fifth ", PublisherYear = 1904, Author = authors[2] }
            };

            context.Set<Book>().AddRange(books);
            context.SaveChanges();
        }
    }
    
    private static async Task SeedDataAsync(DbContext context, bool seed, CancellationToken cancellationToken)
    {
        var hasAuthors = await context.Set<Author>().AnyAsync(cancellationToken);
        if (!hasAuthors)
        {
            var authors = new List<Author>
            {
                new() { Id = 1, Name = "First", DateOfBirth = new DateTime(1999, 9, 9) },
                new() { Id = 2, Name = "Second", DateOfBirth = new DateTime(1875, 11, 11) },
                new() { Id = 3, Name = "Third", DateOfBirth = new DateTime(1860, 1, 29) }
            };

            await context.Set<Author>().AddRangeAsync(authors, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var books = new List<Book>
            {
                new() { Id = 1, Title = "Title first", PublisherYear = 1869, Author = authors[0] },
                new() { Id = 2, Title = "Title second", PublisherYear = 1877, Author = authors[1] },
                new() { Id = 3, Title = "Title third", PublisherYear = 1866, Author = authors[0] },
                new() { Id = 4, Title = "Title fourth", PublisherYear = 1869, Author = authors[0] },
                new() { Id = 5, Title = "Title fifth ", PublisherYear = 1904, Author = authors[2] }
            };

            await context.Set<Book>().AddRangeAsync(books, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}