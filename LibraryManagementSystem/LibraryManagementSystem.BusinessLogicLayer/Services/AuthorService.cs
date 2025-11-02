using LibraryManagementSystem.BusinessLogicLayer.Repositories;
using Models_Author = LibraryManagementSystem.BusinessLogicLayer.Models.Author;

namespace LibraryManagementSystem.BusinessLogicLayer.Services;


public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Models_Author>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task<Models_Author> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        return author;
    }

    public async Task<Models_Author> CreateAuthorAsync(Models_Author author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        return await _authorRepository.CreateAsync(author);
    }

    public async Task UpdateAuthorAsync(Models_Author author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        await _authorRepository.UpdateAsync(author);
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
        return authors;
    }
}