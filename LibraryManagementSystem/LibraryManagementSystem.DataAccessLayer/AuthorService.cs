using LibraryManagementSystem.DataAccessLayer.Entities;
using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;
using Author = LibraryManagementSystem.DataAccessLayer.Entities.Author;
using Models_Author = LibraryManagementSystem.BusinessLogicLayer.Models.Author;

namespace LibraryManagementSystem.DataAccessLayer;


public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _dbContext;

    public AuthorService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Models_Author>> GetAllAuthorsAsync()
    {
        return await _dbContext.Authors.Select(author => FromEntityToModel(author)).ToArrayAsync();
    }

    public async Task<Models_Author?> GetAuthorByIdAsync(Guid id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        return FromEntityToModel(author);
    }

    public async Task<Models_Author> CreateAuthorAsync(Models_Author author)
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

    public async Task UpdateAuthorAsync(Models_Author author)
    {
        var updateAuthor = await _dbContext.Authors.FindAsync(author.Id);
        if (updateAuthor is null)
        {
            throw new ArgumentNullException(nameof(updateAuthor), $"{nameof(updateAuthor)} is null");
        }

        updateAuthor.Name = author.Name;
        updateAuthor.DateOfBirth = author.DateOfBirth;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(Guid id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Models_Author>> FindAuthorsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The name cannot be empty");
        }

        var authors = await _dbContext.Authors
            .Where(author => author.Name.Contains(name))
            .Select(author => FromEntityToModel(author))
            .ToArrayAsync();

        if (!authors.Any())
        {
            throw new InvalidOperationException($"No authors with names containing '{name}' were found");
        }

        return authors;
    }

    public async Task<IEnumerable<AuthorWithCount>> GetAllAuthorsWithBooksCountAsync()
    {
        var authors = await _dbContext.Authors.Select(author => new AuthorWithCount(
            FromEntityToModel(author),
            author.Books.Count())).ToArrayAsync();

        return authors;
    }

    private static Author ToEntity(Models_Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };

    private static Models_Author FromEntityToModel(Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };
}