using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.Local;

public class AuthorService : IAuthorService
{
    private IAuthorService _authorServiceImplementation;
    private static readonly List<Author> Authors = new();

    private static long _idAuthor;

    private long GetNextAuthorId()
    {
        return ++_idAuthor;
    }

    public Task<IEnumerable<Author>> GetAllAuthorsAsync() => Task.FromResult<IEnumerable<Author>>(Authors);
    public Task<Author?> GetAuthorByIdAsync(long id) => Task.FromResult(Authors.FirstOrDefault(p => p.Id == id));

    public Task<Author> CreateAuthorAsync(Author author)
    {
        author.Id = GetNextAuthorId();
        Authors.Add(author);
        return Task.FromResult(author);
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        var updatedAuthor = await GetAuthorByIdAsync(author.Id);
        if (updatedAuthor is null)
        {
            throw new ArgumentException($"Author with ID {author.Id} not found");
        }

        updatedAuthor.Name = author.Name;
        updatedAuthor.DateOfBirth = author.DateOfBirth;
    }

    public async Task DeleteAuthorAsync(long id)
    {
        var deleteAuthor = await GetAuthorByIdAsync(id);
        if (deleteAuthor is null)
        {
            throw new ArgumentException("Author could not be found.");
        }

        Authors.Remove(deleteAuthor);
    }

    public Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuthorWithCount>> GetAllAuthorsWithBooksCountAsync()
    {
        throw new NotImplementedException();
    }
}