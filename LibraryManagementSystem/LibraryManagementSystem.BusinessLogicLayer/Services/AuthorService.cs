using LibraryManagementSystem.DataAccessLayer.Entities;
using LibraryManagementSystem.DataAccessLayer.Repositories;
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
        var authorEntities = await _authorRepository.GetAllAsync();
        return authorEntities.Select(FromEntityToModel);
    }

    public async Task<ModelsAuthor> GetAuthorByIdAsync(Guid id)
    {
        var authorEntity = await _authorRepository.GetByIdAsync(id);
        if (authorEntity is null)
        {
            throw new ArgumentNullException(nameof(authorEntity), $"{nameof(authorEntity)} is null");
        }
        return FromEntityToModel(authorEntity);
    }

    public async Task<ModelsAuthor> CreateAuthorAsync(ModelsAuthor author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        var authorEntity = ToEntity(author);
        var createdEntity = await _authorRepository.CreateAsync(authorEntity);
        return FromEntityToModel(createdEntity);
    }

    public async Task UpdateAuthorAsync(ModelsAuthor author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        var authorEntity = ToEntity(author);
        await _authorRepository.UpdateAsync(authorEntity);
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
        
        var authorEntities = await _authorRepository.FindByNameAsync(name);
        return authorEntities.Select(FromEntityToModel);
    }

    private static Author ToEntity(ModelsAuthor author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth
        };

    private static ModelsAuthor FromEntityToModel(Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
            Books = author.Books?.Select(b => new Models.Book()
            {
                Id = b.Id,
                Title = b.Title,
                PublisherYear = b.PublisherYear,
                AuthorId = author.Id
            })
        };
}