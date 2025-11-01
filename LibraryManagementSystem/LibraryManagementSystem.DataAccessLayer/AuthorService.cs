using LibraryManagementSystem.DataAccessLayer.Repositories;
using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;
using Author = LibraryManagementSystem.DataAccessLayer.Entities.Author;
using Models_Author = LibraryManagementSystem.BusinessLogicLayer.Models.Author;

namespace LibraryManagementSystem.DataAccessLayer;


public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Models_Author>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(FromEntityToModel);
    }

    public async Task<Models_Author?> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
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
        newAuthor = await _authorRepository.CreateAsync(newAuthor);

        author.Id = newAuthor.Id;
        return author;
    }

    public async Task UpdateAuthorAsync(Models_Author author)
    {
        var updateAuthor = await _authorRepository.GetByIdAsync(author.Id);
        if (updateAuthor is null)
        {
            throw new ArgumentNullException(nameof(updateAuthor), $"{nameof(updateAuthor)} is null");
        }

        updateAuthor.Name = author.Name;
        updateAuthor.DateOfBirth = author.DateOfBirth;

        await _authorRepository.UpdateAsync(updateAuthor);
    }

    public async Task DeleteAuthorAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        await _authorRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Models_Author>> FindAuthorsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The name cannot be empty");
        }

        var authors = await _authorRepository.FindByNameAsync(name);
        var authorsList = authors.Select(FromEntityToModel).ToList();

        if (!authorsList.Any())
        {
            throw new InvalidOperationException($"No authors with names containing '{name}' were found");
        }

        return authorsList;
    }

    public async Task<IEnumerable<AuthorWithCount>> GetAllAuthorsWithBooksCountAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        var booksCount = await _authorRepository.GetBooksCountByAuthorAsync();

        return authors.Select(author => new AuthorWithCount(
            FromEntityToModel(author),
            booksCount.GetValueOrDefault(author.Id, 0)));
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