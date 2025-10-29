using LibraryManagementSystem.Services.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Book = LibraryManagementSystem.Services.Models.Book;

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
        return await _dbContext.Authors.Select(author => FromEntityToModel(author)).ToArrayAsync();
    }

    public async Task<Models.Author?> GetAuthorByIdAsync(long id)
    {
       var author = await _dbContext.Authors.FindAsync(id);
       return author is null ? null : FromEntityToModel(author);
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

    public async Task UpdateAuthorAsync(Models.Author author)
    {
        // var updateAuthor = await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
        var updateAuthor = await _dbContext.Authors.FindAsync(author.Id);
        if (updateAuthor is null)
        {
            throw new ArgumentNullException(nameof(updateAuthor), $"{nameof(updateAuthor)} is null");
        }
        updateAuthor.Name = author.Name;
        updateAuthor.DateOfBirth = author.DateOfBirth;
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DbUpdateConcurrencyException(e.Message, e);
        }  
        
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

    private static Models.Author FromEntityToModel(Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };



}