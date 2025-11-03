using LibraryManagementSystem.BusinessLogicLayer.Repositories;
using ModelsAuthor = LibraryManagementSystem.BusinessLogicLayer.Models.Author;

namespace LibraryManagementSystem.BusinessLogicLayer.Services;


public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<ModelsAuthor>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task<ModelsAuthor> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        return author;
    }

    public async Task<ModelsAuthor> CreateAuthorAsync(ModelsAuthor author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        return await _authorRepository.CreateAsync(author);
    }

    public async Task UpdateAuthorAsync(ModelsAuthor author)
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

    public async Task<IEnumerable<ModelsAuthor>> FindAuthorsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The name cannot be empty");
        }
        var authors = await _authorRepository.FindByNameAsync(name);
        return authors;
    }
}