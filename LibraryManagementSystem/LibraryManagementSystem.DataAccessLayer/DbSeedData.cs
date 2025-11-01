using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer;

internal static class DbSeedData
{
    private static Author[] _authors = new Author[]
    {
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "First", DateOfBirth = new DateTime(1999, 9, 9) },
        new() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Second", DateOfBirth = new DateTime(1875, 11, 11) },
        new() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Third", DateOfBirth = new DateTime(1860, 1, 29) }
    };
    
    private static Book[] _books = new Book[]
    {
        new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), Title = "Title first", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), Title = "Title second", PublisherYear = 1877, Author = _authors[1] },
        new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), Title = "Title third", PublisherYear = 1866, Author = _authors[0] },
        new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), Title = "Title fourth", PublisherYear = 1869, Author = _authors[0] },
        new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), Title = "Title fifth ", PublisherYear = 1904, Author = _authors[2] }
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