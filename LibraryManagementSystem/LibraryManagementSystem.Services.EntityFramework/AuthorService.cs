using System.Linq.Expressions;
using LibraryManagementSystem.Services.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.EntityFramework;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _dbContext;

    public AuthorService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models.Author>> GetAllAuthorsAsync()
    {
        return await _dbContext.Authors.Select(user => MapUserToServiceEntity(user)).ToArrayAsync();
    }

    public async Task<Models.Author?> GetAuthorByIdAsync(long id)
    {
       var author = await _dbContext.Authors.FindAsync(id);
       return author is null ? null : MapUserToServiceEntity(author);
    }

    public async Task<Models.Author> CreateAuthorAsync(Models.Author author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        var newAuthor = ToEntity(author);
         _dbContext.Authors.Add(newAuthor);
         await _dbContext.SaveChangesAsync();
        
        author.Id = newAuthor.Id;
        return author;
    }

    public Task UpdateAuthorAsync(Models.Author author)
    {
        throw new NotImplementedException();
    }
    

    public async Task DeleteAuthorAsync(long id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        
        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
    
    private static Author ToEntity(Models.Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

    private static Models.Author MapUserToServiceEntity(Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };
    
    
}