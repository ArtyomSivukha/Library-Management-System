using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer;

internal static class DbSeedData
{
    private static Author[] _authors = new Author[]
    {
        new() { Id = 1, Name = "First", DateOfBirth = new DateTime(1999, 9, 9) },
        new() { Id = 2, Name = "Second", DateOfBirth = new DateTime(1875, 11, 11) },
        new() { Id = 3, Name = "Third", DateOfBirth = new DateTime(1860, 1, 29) }
    };
    
    private static Book[] _books = new Book[]
    {
        new() { Id = 1, Title = "Title first", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = 2, Title = "Title second", PublisherYear = 1877, Author = _authors[1] },
        new() { Id = 3, Title = "Title third", PublisherYear = 1866, Author = _authors[0] },
        new() { Id = 4, Title = "Title fourth", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = 5, Title = "Title fifth ", PublisherYear = 1904, Author = _authors[2] }
    };
    
    public static void SeedData(DbContext context, bool seed)
    {
        var hasAuthors = context.Set<Author>().Any();
        if (!hasAuthors)
        {
            context.Set<Author>().AddRange(_authors);
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors ON");
            
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors OFF");
            
            context.Set<Book>().AddRange(_books);
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books ON");
            
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books OFF");
        }
    }
    
    public static async Task SeedDataAsync(DbContext context, bool seed, CancellationToken ct)
    {
        var hasAuthors = await context.Set<Author>().AnyAsync(ct);
        if (!hasAuthors)
        {
            await context.Set<Author>().AddRangeAsync(_authors, ct);
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Authors ON", ct);
            
            await context.SaveChangesAsync(ct);
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Authors OFF", ct);
            
            await context.Set<Book>().AddRangeAsync(_books, ct);
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Books ON", ct);
            
            await context.SaveChangesAsync(ct);
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Books OFF", ct);
        }
    }
}