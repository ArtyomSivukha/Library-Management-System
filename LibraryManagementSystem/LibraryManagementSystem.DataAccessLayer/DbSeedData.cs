using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer;

internal static class DbSeedData
{
    private static Author[] _authors = new Author[]
    {
        new() { Id = Guid.NewGuid(), Name = "First", DateOfBirth = new DateTime(1999, 9, 9) },
        new() { Id = Guid.NewGuid(), Name = "Second", DateOfBirth = new DateTime(1875, 11, 11) },
        new() { Id = Guid.NewGuid(), Name = "Third", DateOfBirth = new DateTime(1860, 1, 29) }
    };
    
    private static Book[] _books = new Book[]
    {
        new() { Id = Guid.NewGuid(), Title = "Title first", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = Guid.NewGuid(), Title = "Title second", PublisherYear = 1877, Author = _authors[1] },
        new() { Id = Guid.NewGuid(), Title = "Title third", PublisherYear = 1866, Author = _authors[0] },
        new() { Id = Guid.NewGuid(), Title = "Title fourth", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = Guid.NewGuid(), Title = "Title fifth ", PublisherYear = 1904, Author = _authors[2] }
    };
    
    public static void SeedData(DbContext context, bool seed)
    {
        var hasAuthors = context.Set<Author>().Any();
        if (!hasAuthors)
        {
            context.Set<Author>().AddRange(_authors);
            context.SaveChanges();
            
            context.Set<Book>().AddRange(_books);
            context.SaveChanges();
        }
    }
    
    public static async Task SeedDataAsync(DbContext context, bool seed, CancellationToken ct)
    {
        var hasAuthors = await context.Set<Author>().AnyAsync(ct);
        if (!hasAuthors)
        {
            await context.Set<Author>().AddRangeAsync(_authors, ct);
            await context.SaveChangesAsync(ct);
            
            await context.Set<Book>().AddRangeAsync(_books, ct);
            await context.SaveChangesAsync(ct);
        }
    }
}